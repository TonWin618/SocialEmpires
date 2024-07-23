using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Translations;
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
            
            var records = _appDbContext
                .TranslationRecords
                .Where(_ => _.Language == CultureInfo.CurrentCulture.Name)
                .OrderByDescending(_ => _.Id)
                .Page(pageIndex, pageSize, out var pageCount)
                .ToList();
            foreach (var record in records) 
            {
                var targetType = _configService
                    .GetType()
                    .GetProperty(record.Section)
                    .PropertyType
                    .GetGenericArguments()
                    .First();
                var item = await _appDbContext.FindAsync(targetType, record.ItemId);
                var targetProperty = targetType.GetProperty(record.Property).GetValue(item) as MultiLanguageString;
                record.Current = targetProperty.Get(CultureInfo.CurrentCulture.Name);
            }

            ViewData["PageData"] = records;
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

        [HttpPost]
        public async Task<IActionResult> ApplyTranslation(int id)
        {
            var translationRecord = await _appDbContext.TranslationRecords.FindAsync(id);
            if (translationRecord == null)
            {
                return NotFound();
            }

            var targetType = _configService
                .GetType()
                .GetProperty(translationRecord.Section)
                .PropertyType
                .GetGenericArguments()
                .First();

            if (targetType == null)
            {
                return NotFound();
            }

            var item = _appDbContext.Find(targetType, translationRecord.ItemId);
            var targetPropertyInfo = item.GetType().GetProperty(translationRecord.Property);
            var targetProperty = targetPropertyInfo.GetValue(item) as MultiLanguageString;
            targetProperty.Set(translationRecord.Language, translationRecord.Translation);

            translationRecord.Approved = true;

            return this.Redirect();
        }
    }
}
