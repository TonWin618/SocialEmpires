using Microsoft.AspNetCore.Mvc;

namespace SocialEmpires.Controllers
{
    public partial class AdminController
    {
        [HttpGet]
        public IActionResult Bulletin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PublishBulletin([FromForm]string htmlContent)
        {
            return Redirect(Request.Headers.Referer);
        }
    }
}
