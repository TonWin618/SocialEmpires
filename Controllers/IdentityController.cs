using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.EmailSender;

namespace SocialEmpires.Controllers
{
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> _logger;
        private string[] LoginMethods { get; set; } = ["EmailAndPassword", "EmailAndToken"];

        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }

        #region Initialize
        [HttpGet]
        public IActionResult Initialize()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Initialize(
            string email,
            string password,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] RoleManager<IdentityRole> _roleManager)
        {
            if(email == null || password == null)
            {
                return View("Initialize");
            }

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

            return View("Login");
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login(string? method)
        {
            if (!LoginMethods.Contains(method)) 
            {
                method = LoginMethods.First();
            }
            ViewData["LoginMethod"] = method;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginByEmailAndPassword(
            string email,
            string password,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager)
        {
            ViewData["Email"] = email;
            ViewData["LoginMethod"] = "EmailAndPassword";

            if (email == null)
            {
                ViewData["ErrorMessage"] = "EmailIsRequired";
                return View("Login");
            }

            if (password == null)
            {
                ViewData["ErrorMessage"] = "PasswordIsRequired";
                return View("Login");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "UserNotFound";
                return View("Login");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            if (!result.Succeeded)
            {
                ViewData["ErrorMessage"] = "InvalidPassword";
                return View("Login");
            }
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> LoginByEmailAndToken(
            string email,
            string token,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager)
        {
            ViewData["Email"] = email;
            ViewData["Token"] = token;

            if (email == null)
            {
                ViewData["ErrorMessage"] = "EmailIsRequired";
                return View("Login");
            }

            if (token == null)
            {
                ViewData["ErrorMessage"] = "TokenIsRequired";
                return View("Login");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "UserNotFound";
                return View("Login");
            }

            if (!(await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, token)))
            {
                ViewData["ErrorMessage"] = "InvalidToken";
                return View("Login");
            }

            await _signInManager.SignInAsync(user, true);
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> SendLoginTokenEmail(
            string email,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager,
            [FromServices] IEmailSender _emailSender)
        {
            ViewData["Email"] = email;
            ViewData["LoginMethod"] = "EmailAndToken";

            if (email == null)
            {
                ViewData["ErrorMessage"] = "EmailIsRequired";
                return View("Login");
            }

            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "UserNotFound";
                return View("Login");
            }

            if (!user.EmailConfirmed)
            {
                ViewData["ErrorMessage"] = "EmailNotConfirmed";
                return View("Login");
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);

            await _emailSender.SendAsync(
                email,
                "[Social Empires] Verification code for login",
                $"<html><body><h4>Your verification code is </h4><h1>{token}</h1><br/></body></html>");

            ViewData["SendingInterval"] = 60;

            return View("Login");
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(
            string password,
            string token,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager)
        {
            ViewData["Token"] = token;
            ViewData["Password"] = password;

            if (token == null)
            {
                ViewData["ErrorMessage"] = "TokenIsRequired";
                return View("Register");
            }

            if (password == null)
            {
                ViewData["ErrorMessage"] = "PasswordIsRequired";
                return View("Register");
            }

            if (HttpContext?.User?.Identity?.Name == null)
            {
                return View("Register");
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user == null)
            {
                return View("Register");
            }

            var validatePasswordResult = await _userManager.PasswordValidators
                .First()
                .ValidateAsync(_userManager, user, password);

            if (!validatePasswordResult.Succeeded)
            {
                ViewData["ErrorMessage"] = "PasswordSetFailed";
                return View("Register");
            }

            var emailConfrimResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!emailConfrimResult.Succeeded)
            {
                ViewData["ErrorMessage"] = "InvalidToken";
                return View("Register");
            }

            await _userManager.ResetPasswordAsync(
                user, 
                await _userManager.GeneratePasswordResetTokenAsync(user), 
                password);

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.SetTwoFactorEnabledAsync(user, true);

            await _signInManager.SignOutAsync();

            ViewData["Email"] = user.Email;
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailConfirmationEmail(
            string email,
            [FromServices] UserManager<IdentityUser> _userManager,
            [FromServices] SignInManager<IdentityUser> _signInManager,
            [FromServices] IEmailSender _emailSender)
        {
            ViewData["Email"] = email;

            if (email == null)
            {
                ViewData["ErrorMessage"] = "EmailIsRequired";
                return View("Register");
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
                    ViewData["ErrorMessage"] = string.Join('\n', result.Errors.Select(_ => _.Description));
                    return View("Register");
                }

                await _signInManager.SignInAsync(user, isPersistent: true);
                return View("Register");
            }
            else
            {
                if (user.EmailConfirmed)
                {
                    ViewData["ErrorMessage"] = "UserExisted";
                    return View("Login");
                }

                await _signInManager.SignInAsync(user, isPersistent: true);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                await _emailSender.SendAsync(
                    email,
                    "[Social Empires] Verification code for register",
                    $"<html><body><h4>Your verification code is </h4><h1>{token}</h1><br/></body></html>");

                ViewData["SendingInterval"] = 60;
                return View("Register");
            }
        }

        #endregion
    }
}
