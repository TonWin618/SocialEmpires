using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Utils;

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
    }
}
