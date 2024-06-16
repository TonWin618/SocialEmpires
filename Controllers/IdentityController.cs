using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models;

namespace SocialEmpires.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(
            AppDbContext appDbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityController> logger) 
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost("api/initialize")]
        public async Task<IActionResult> Initialize(
            [FromForm] string email,
            [FromForm] string password)
        {
            if(_roleManager.Roles.Any(_ => _.Name == "Admin"))
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

        [HttpPost("api/register")]
        [Authorize]
        public async Task<IActionResult> RegisterByEmailAndPassword(
            [FromForm] string password,
            [FromForm] string code)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if(user == null)
            {
                return Redirect("/Login");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "ConfirmEmailFailed";
                return Redirect("/Register");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
            if(!resetPasswordResult.Succeeded)
            {
                TempData["ErrorMessage"] = "PasswordSetFailed";
                return Redirect("/Register");
            }
            
            await _userManager.AddToRoleAsync(user, "User");

            return Redirect("/");
        }

        [HttpPost("api/login")]
        public async Task<IActionResult> LoginByEmailAndPassword(
            [FromForm] string email, 
            [FromForm] string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
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

        [HttpPost("api/sendEmailConfirmationEmail")]
        public async Task<IActionResult> SendEmailConfirmationEmail(
            [FromForm]string email)
        {
            if(HttpContext.User.Identity.Name != null)
            {
                var loginUser = await _userManager.FindByIdAsync(HttpContext.User.Identity.Name);
                if(loginUser.EmailConfirmed)
                {
                    return Redirect("/Privacy");
                }
            }

            var user = new IdentityUser()
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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _logger.LogInformation($"Confirmation token for {user.Id}: {token}");
            TempData["SendingInterval"] = 60;
            return Redirect("/Register");
        }
    }
}
