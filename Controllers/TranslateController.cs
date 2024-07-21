using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Services;
using SocialEmpires.Utils;
using System.Collections;
using System.Globalization;

namespace SocialEmpires.Controllers
{
    public class TranslateController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ConfigService _configService;

        public TranslateController(
            AppDbContext appDbContext,
            ConfigService configService)
        {
            _appDbContext = appDbContext;
            _configService = configService;
        }

        public IActionResult Index(
            string section = "Items",
            int pageIndex = 1,
            int pageSize = 20)
        {
            var targetPropertyInfo = _configService.GetType().GetProperty(section);
            if (targetPropertyInfo == null)
            {
                return NotFound();
            }

            var targetProperty = targetPropertyInfo.GetValue(_configService) as IEnumerable<object>;
            if (targetProperty == null)
            {
                return NotFound();
            }

            var multiLanguagePropertyInfos = targetProperty
                .GetType()
                .GetGenericArguments().First()
                .GetProperties()
                .Where(p => p.PropertyType == typeof(MultiLanguageString));

            var pageCount = 0;
            var translationItemDtos = new List<TranslationItemDto>();
            foreach (var item in targetProperty.Page(pageIndex, pageSize, out pageCount))
            {
                var strings = new List<TranslationString>();
                foreach (var propertyInfo in multiLanguagePropertyInfos)
                {
                    var multiLanguageString = propertyInfo.GetValue(item) as MultiLanguageString;
                    strings.Add(new TranslationString(
                        propertyInfo.Name,
                        multiLanguageString.Get(SupportLanguages.Default),
                        multiLanguageString.Get(CultureInfo.CurrentCulture.Name)));
                }
                translationItemDtos.Add(new TranslationItemDto(
                    item.GetType().GetProperty("Id").GetValue(item).ToString(),
                    strings));
            }

            ViewData["Section"] = section;
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            ViewData["PageCount"] = pageCount;
            ViewData["PageData"] = translationItemDtos;
            return View();
        }

        public record TranslationItemDto(string Id, List<TranslationString> Strings);
        public record TranslationString(string Property, string? Origin, string? Translation);
    }
}
