using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminController :Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.UseAdmin = true;
            ViewBag.UseManager = false;
            return View();
        }
    }
}
