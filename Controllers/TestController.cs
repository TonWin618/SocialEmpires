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
            var itemSeed = new ItemDataSeed(_appDbContext);
            itemSeed.Initialize();
            var levelSeed = new LevelDataSeed(_appDbContext);
            levelSeed.Initialize();
            var missionSeed = new MissionDataSeed(_appDbContext);
            missionSeed.Initialize();
            var expansionPriceSeed = new ExpansionPriceDataSeed(_appDbContext);
            expansionPriceSeed.Initialize();
            return Ok();
        }
    }
}
