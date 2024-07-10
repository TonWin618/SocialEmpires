using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Bulletins;

namespace SocialEmpires.Controllers
{
    public class TestController : Controller
    {
        [HttpPost]
        public Bulletin MultiLanguageTest([MultiLanguage] Bulletin bulletin)
        {
            return bulletin;
        }
    }
}
