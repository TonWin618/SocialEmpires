using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.EmailSender;

namespace SocialEmpires.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }

        [HttpPost("api/initialize")]
        public async Task<IActionResult> Initialize(
            [FromForm] string email,
            [FromForm] string password,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] RoleManager<IdentityRole> _roleManager)
        {
            if (_roleManager.Roles.Any(_ => _.Name == "Admin"))
            {
                return Redirect("/");
            }
            await _roleManager.CreateAsync(new IdentityRole("User"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            var user = new IdentityUser()
            {
                Email = email
            };
            user.UserName = user.Id.ToString();
            await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(user, "Admin");

            return Redirect("/Login");
        }

        [Authorize]
        [HttpPost("api/register")]
        public async Task<IActionResult> RegisterByEmailAndPassword(
            [FromForm] string password,
            [FromForm] string code,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager)
        {
            TempData["Code"] = code;
            TempData["Password"] = password;

            if (HttpContext?.User?.Identity?.Name == null)
            {
                return Redirect("/Register");
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user == null)
            {
                return Redirect("/Register");
            }

            var validatePasswordResult = await _userManager.PasswordValidators
                .First()
                .ValidateAsync(_userManager, user, password);

            if (!validatePasswordResult.Succeeded)
            {
                TempData["ErrorMessage"] = "PasswordSetFailed";
                return Redirect("/Register");
            }

            var emailConfrimResult = await _userManager.ConfirmEmailAsync(user, code);
            if (!emailConfrimResult.Succeeded)
            {
                TempData["ErrorMessage"] = "InvalidAuthCode";
                return Redirect("/Register");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, password);

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.SetTwoFactorEnabledAsync(user, true);

            await _signInManager.SignOutAsync();

            return Redirect("/Login");
        }

        [HttpPost("api/loginByEmailAndPassword")]
        public async Task<IActionResult> LoginByEmailAndPassword(
            [FromForm] string email,
            [FromForm] string password,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager)
        {
            TempData["Email"] = email;

            if (email == null)
            {
                TempData["ErrorMessage"] = "EmailIsRequired";
                return Redirect("/Login");
            }

            if (password == null)
            {
                TempData["ErrorMessage"] = "PasswordIsRequired";
                return Redirect("/Login");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "UserNotFound";
                return Redirect("/Login");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            if (result.Succeeded)
            {
                return Redirect("/");
            }
            else
            {
                ViewData["ErrorMessage"] = "InvalidPassword";
                return Redirect("/Login");
            }
        }

        [HttpPost("api/loginByEmailAndToken")]
        public async Task<IActionResult> LoginByEmailAndToken(
            [FromForm] string email,
            [FromForm] string token,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;

            if (email == null)
            {
                TempData["ErrorMessage"] = "EmailIsRequired";
                return Redirect("/TokenLogin");
            }

            if (token == null)
            {
                TempData["ErrorMessage"] = "TokenIsRequired";
                return Redirect("/TokenLogin");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "UserNotFound";
                return Redirect("/TokenLogin");
            }
            
            var result = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, token);
            if (result)
            {
                await _signInManager.SignInAsync(user, true);
                return Redirect("/");
            }
            else
            {
                TempData["ErrorMessage"] = "InvalidAuthCode";
                return Redirect("/TokenLogin");
            }
        }

        [HttpPost("api/sendEmailConfirmationEmail")]
        public async Task<IActionResult> SendEmailConfirmationEmail(
            [FromForm] string email,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager,
            [FromServices] IEmailSender _emailSender)
        {
            if (email == null)
            {
                TempData["ErrorMessage"] = "EmailIsRequired";
                return Redirect("/Register");
            }

            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new IdentityUser()
                {
                    Email = email
                };
                user.UserName = user.Id.ToString();
                var result = await _userManager.CreateAsync(user, Guid.NewGuid().ToString());
                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = string.Join('\n', result.Errors.Select(_ => _.Description));
                    return Redirect("/Register");
                }

                await _signInManager.SignInAsync(user, isPersistent: true);
                return Redirect("/Register");
            }
            else
            {
                if (user.EmailConfirmed)
                {
                    TempData["ErrorMessage"] = "UserExisted";
                    return Redirect("/Login");
                }

                await _signInManager.SignInAsync(user, isPersistent: true);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                _logger.LogInformation("Confirmation token for {user.Id}: {token}", user.Id, token);

                await _emailSender.SendAsync(
                    email,
                    "[Social Empires] Verification code for register",
                    $"<html><body><h4>Your verification code is </h4><h1>{token}</h1><br/></body></html>");

                TempData["SendingInterval"] = 60;
                return Redirect("/Register");
            }
        }

        [HttpPost("api/sendLoginTokenEmail")]
        public async Task<IActionResult> SendLoginTokenEmail(
            [FromForm] string email,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager,
            [FromServices] IEmailSender _emailSender)
        {
            if (email == null) 
            {
                TempData["ErrorMessage"] = "EmailIsRequired";
                return Redirect("/TokenLogin");
            }
            TempData["Email"] = email;

            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "UserNotFound";
                return Redirect("/TokenLogin");
            }

            if (!user.EmailConfirmed)
            {
                TempData["ErrorMessage"] = "EmailNotConfirmed";
                return Redirect("/TokenLogin");
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);

            await _emailSender.SendAsync(
                email,
                "[Social Empires] Verification code for login",
                $"<html><body><h4>Your verification code is </h4><h1>{token}</h1><br/></body></html>");

            TempData["SendingInterval"] = 60;

            return Redirect("/TokenLogin");
        }
    }
}
