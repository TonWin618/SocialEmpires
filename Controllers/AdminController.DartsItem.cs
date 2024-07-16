using Microsoft.AspNetCore.Mvc;
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
            (ViewData["PageCount"], ViewData["PageData"]) = PageHelper.Page(pageIndex, pageSize, _configFileService.DartsItems);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteDartsItem(int id)
        {
            var dartsItem = _appDbContext.DartsItems.Find(id);
            _appDbContext.DartsItems.Remove(dartsItem);
            return Redirect(Request.Headers.Referer);
        }
    }
}
