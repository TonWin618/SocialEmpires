using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialEmpires.Models;
using SocialEmpires.Pages;
using SocialEmpires.Pages.Shared;
using System.Security.Cryptography;

namespace SocialEmpires.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(
            AppDbContext appDbContext,
            UserManager<IdentityUser> userManager) 
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task RegisterByEmailAndPassword(
            [FromForm] RegisterByEmailAndPasswordRequest request)
        {
            var user = new IdentityUser()
            {
                Email = request.email,
            };
            await _userManager.CreateAsync(user, request.password);
        }

        public record RegisterByEmailAndPasswordRequest(string email, string password);

        [HttpPost("Login")]
        public async Task<IActionResult> LoginByEmailAndPassword(
            [FromForm] LoginByEmailAndPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if(user == null)
            {
                TempData["ErrorMessage"] = "UserNotFound";
                return Redirect("/Login");
            }

            if(await _userManager.CheckPasswordAsync(user, request.password))
            {
                return Redirect("/");
            }
            else
            {
                ViewData["ErrorMessage"] = "InvalidPassword";
                return Redirect("/Login");
            }
        }
        public record LoginByEmailAndPasswordRequest(string email, string password);
    }
}
