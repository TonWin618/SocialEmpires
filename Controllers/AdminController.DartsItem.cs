using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;

namespace SocialEmpires.Controllers
{
    public partial class AdminController
    {
        [HttpGet]
        public IActionResult DartsItems(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            (ViewData["PageCount"], ViewData["PageData"]) = PageHelper.Page(pageIndex, pageSize, _configService.DartsItems);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDartsItem(int id)
        {
            var dartsItem = await _appDbContext.DartsItems.FindAsync(id);
            _appDbContext.DartsItems.Remove(dartsItem);
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        public async Task<IActionResult> AddDartsItem(int item1, int item2, int item3, int item4, int item5, int item6, int extraItem)
        {
            var dartsItem = new DartsItem()
            {
                Items = [item1, item2, item3, item4, item5, item6],
                ExtraItem = extraItem,
                StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };
            await _appDbContext.AddAsync(dartsItem);
            return Redirect(Request.Headers.Referer);
        }
    }
}
