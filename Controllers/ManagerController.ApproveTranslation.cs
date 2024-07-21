using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Notifications;
using SocialEmpires.Utils;
using System.Globalization;

namespace SocialEmpires.Controllers
{
    public partial class ManagerController
    {
        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> DeleteTranslation(int id)
        {
            var translationRecord = await _appDbContext.TranslationRecords.FindAsync(id);
            if(translationRecord == null)
            {
                return NotFound();
            }
            _appDbContext.Remove(translationRecord);
            return this.Redirect();
        }

    }
}
