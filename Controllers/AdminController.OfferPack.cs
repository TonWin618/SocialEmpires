using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json;

namespace SocialEmpires.Controllers
{
    public partial class AdminController
    {
        [HttpGet]
        public IActionResult OfferPacks(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            (ViewData["PageCount"], ViewData["PageData"]) = PageHelper.Page(pageIndex, pageSize, _configFileService.OfferPacks);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnableOfferPack(int id)
        {
            var offerPack = await _appDbContext.OfferPacks.FindAsync(id);
            offerPack.Enabled = true;
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        public async Task<IActionResult> DisableOfferPack(int id)
        {
            var offerPack = await _appDbContext.OfferPacks.FindAsync(id);
            offerPack.Enabled = false;
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOfferPack(int id)
        {
            var offerPack = await _appDbContext.OfferPacks.FindAsync(id);
            _appDbContext.Remove(offerPack);
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        public async Task<IActionResult> AddOfferPack(int packType, int position, int mana, int costCash, int gold, int stone, int wood, int food, int xp, string items)
        {
            var jsonOptions = new JsonSerializerOptions();

            jsonOptions.Converters.Add(new IntListOrIntListListConverter());
            var offerPack = new OfferPack()
            {
                Position = position,
                CostCash = costCash,
                Gold = gold,
                Stone = stone,
                Food = food,
                Wood = wood,
                Xp = xp,
                Items = JsonSerializer.Deserialize<List<object>>(items),
                Enabled = true,
                PackType = packType,
            };
            await _appDbContext.AddAsync(offerPack);

            return Redirect(Request.Headers.Referer);
        }
    }
}
