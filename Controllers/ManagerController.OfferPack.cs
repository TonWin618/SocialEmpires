using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models.Options;
using SocialEmpires.Utils;
using System.Text.Json;

namespace SocialEmpires.Controllers
{
    public partial class ManagerController
    {
        [HttpGet]
        public IActionResult OfferPacks(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            (ViewData["PageCount"], ViewData["PageData"]) = PageHelper.Page(pageIndex, pageSize, _configService.OfferPacks);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "OfferPackManager")]
        public async Task<IActionResult> EnableOfferPack(int id)
        {
            var offerPack = await _appDbContext.OfferPacks.FindAsync(id);
            offerPack.Enabled = true;
            return this.Redirect();
        }

        [HttpPost]
        [Authorize(Roles = "OfferPackManager")]
        public async Task<IActionResult> DisableOfferPack(int id)
        {
            var offerPack = await _appDbContext.OfferPacks.FindAsync(id);
            offerPack.Enabled = false;
            return this.Redirect();
        }

        [HttpPost]
        [Authorize(Roles = "OfferPackManager")]
        public async Task<IActionResult> DeleteOfferPack(int id)
        {
            var offerPack = await _appDbContext.OfferPacks.FindAsync(id);
            _appDbContext.Remove(offerPack);
            return this.Redirect();
        }

        [HttpPost]
        [Authorize(Roles = "OfferPackManager")]
        public async Task<IActionResult> AddOfferPack(
            int packType, int position, int costCash, 
            int gold, int stone, int wood, int food, int xp, int mana, string items,
            IFormFile image, 
            [FromServices] IWebHostEnvironment environment, 
            [FromServices] IOptions<FileDirectoriesOptions> options)
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
                Mana = mana,
                Items = JsonSerializer.Deserialize<List<object>>(items, jsonOptions),
                Enabled = true,
                PackType = packType,
            };
            var offerPackEntry = await _appDbContext.AddAsync(offerPack);
            _appDbContext.SaveChanges();

            if (image != null)
            {
                var filePath = Path.Combine(environment.ContentRootPath, options.Value.Assets, "images_new", "en", $"offer_pack_{offerPackEntry.Entity.Id}.png");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            return this.Redirect();
        }
    }
}
