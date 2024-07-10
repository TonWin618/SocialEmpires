using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Bulletins;
using SocialEmpires.Models.Seeds;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    public class TestController : Controller
    {
        [HttpPost]
        public IActionResult MultiLanguageTest([MultiLanguage] Bulletin bulletin)
        {
            return this.JsonWithLanguage(bulletin);
        }

        [HttpGet]
        public IActionResult DataSeed(
            [FromServices] ConfigFileService _configFileService,
            [FromServices] AppDbContext _appDbContext)
        {
            var seed = new ItemDataSeed(_appDbContext);
            seed.Initialize();
            return Ok();
        }
    }
}
