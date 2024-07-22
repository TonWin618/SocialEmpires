using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminController :Controller
    {
        private readonly AppDbContext _appDbContext;

        public AdminController(
            AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
