using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SocialEmpires.Controllers
{
    public partial class IdentityController
    {
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
            if (email == null || password == null)
            {
                return View("Initialize");
            }

            if (_roleManager.Roles.Any(_ => _.Name == "Admin"))
            {
                return Redirect("/");
            }
            await _roleManager.CreateAsync(new IdentityRole("User"));
            await _roleManager.CreateAsync(new IdentityRole("Manager"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            var user = new IdentityUser()
            {
                Email = email
            };
            user.UserName = user.Id.ToString();
            await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(user, "Manager");
            await _userManager.AddToRoleAsync(user, "Admin");

            return View("Login");
        }
    }
}
