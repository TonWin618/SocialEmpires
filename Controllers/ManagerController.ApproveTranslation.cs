using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Notifications;
using SocialEmpires.Utils;
using System.Globalization;

namespace SocialEmpires.Controllers
{
    public partial class ManagerController
    {
        public async Task<IActionResult> ApproveTranslation(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            ViewData["PageData"] = _appDbContext
                .TranslationRecords
                .Where(_ => _.Language == CultureInfo.CurrentCulture.Name)
                .Page(pageIndex, pageSize, out var pageCount)
                .ToList();
            ViewData["PageCount"] = pageCount;
            return View();
        }
    }
}
