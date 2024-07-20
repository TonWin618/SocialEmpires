using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Dtos;
using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Controllers
{
    [Authorize("Admin")]
    public class TestController : Controller
    {
        [HttpPost]
        public IActionResult MultiLanguageTest([MultiLanguage] NotificationDto bulletin)
        {
            return this.JsonWithLanguage(bulletin);
        }
    }
}
