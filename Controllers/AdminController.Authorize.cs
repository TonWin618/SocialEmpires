using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Utils;
using System.Data;

namespace SocialEmpires.Controllers
{
    public partial class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] RoleManager<IdentityRole> roleManager,
            int pageIndex = 1, 
            int pageSize = 20)
        {
            ViewData["Roles"] = roleManager.Roles.Select(_=>_.Name).ToList();

            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            ViewData["PageData"] = userManager
                .Users
                .Page(pageIndex, pageSize, out var pageCount)
                .Select(u => new UserDto(u.Id, u.Email, u.EmailConfirmed, userManager.GetRolesAsync(u).Result.ToList()))
                .ToList();
            ViewData["PageCount"] = pageCount;
            return View();
        }

        public record UserDto(string Id, string Email, bool EmailConfirmed, List<string> Roles);

        public async Task<IActionResult> AddAuthorize(
            [FromServices] UserManager<IdentityUser> userManager,
            string userId, 
            string role)
        {
            if (role == "Admin")
            {
                ViewData["ErrorMessage"] = "InvalidOperation";
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) 
            {
                ViewData["ErrorMessage"] = "UserNotFound";
                return this.Redirect();
            }
            
            var result = await userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                ViewData["ErrorMessage"] = "RoleNotFound";
            }
            return this.Redirect();
        }

        public async Task<IActionResult> RemoveAuthorize(
            [FromServices] UserManager<IdentityUser> userManager,
            string userId,
            string role)
        {
            if (role == "Admin")
            {
                ViewData["ErrorMessage"] = "InvalidOperation";
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "UserNotFound";
                return this.Redirect();
            }

            var result = await userManager.RemoveFromRoleAsync(user, role);
            if (!result.Succeeded)
            {
                ViewData["ErrorMessage"] = "RoleNotFound";
            }
            return this.Redirect();
        }

        public async Task<IActionResult> CreateRole(
            [FromServices] RoleManager<IdentityRole> roleManager,
            string role)
        {
            var result = await roleManager.CreateAsync(new IdentityRole(role));
            if (!result.Succeeded)
            {
                ViewData["ErrorMessage"] = "CreateFaild";
            }
            return this.Redirect();
        }
    }
}
