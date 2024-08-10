using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Translations;
using SocialEmpires.Services;
using SocialEmpires.Utils;
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

        [HttpGet]
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
                var itemId = (int)item.GetType().GetProperty("Id").GetValue(item);
                var strings = new List<TranslationString>();
                foreach (var propertyInfo in multiLanguagePropertyInfos)
                {
                    var multiLanguageString = propertyInfo.GetValue(item) as MultiLanguageString;
                    var approvingTranslation = _appDbContext.TranslationRecords
                        .Where(_ =>
                            _.Section == section &&
                            _.ItemId == itemId &&
                            _.Property == propertyInfo.Name &&
                            _.Language == CultureInfo.CurrentCulture.Name)
                        .Select(_ => _.Translation)
                        .ToList();
                    strings.Add(new TranslationString(
                        propertyInfo.Name,
                        multiLanguageString.Get(SupportLanguages.Default),
                        multiLanguageString.Get(CultureInfo.CurrentCulture.Name),
                        approvingTranslation));
                }
                translationItemDtos.Add(new TranslationItemDto(itemId, strings));
            }

            ViewData["Section"] = section;
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            ViewData["PageCount"] = pageCount;
            ViewData["PageData"] = translationItemDtos;
            return View();
        }

        public record TranslationItemDto(int Id, List<TranslationString> Strings);
        public record TranslationString(string Property, string? Origin, string? Translation, List<string> ApprovingTranslations);

        [HttpPost]
        public async Task<IActionResult> SubmitTranslation(int id, string section, string property, string origin, string translation)
        {
            await _appDbContext.TranslationRecords.AddAsync(new TranslationRecord
            {
                ItemId = id,
                Section = section,
                Property = property,
                Origin = origin,
                Translation = translation,
                Language = CultureInfo.CurrentCulture.Name,
                SubmitterId = User.Identity?.Name ?? "",
                Approved = false
            });
            return this.Redirect();
        }

        [HttpGet]
        public async Task<IActionResult> FindTranslation(
            string query,
            string section = "Items",
            int pageIndex = 1,
            int pageSize = 20)
        {
            if(query.IsNullOrEmpty() || section.IsNullOrEmpty())
            {
                return NotFound();
            }

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

            var multiLanguagePropertyNames = multiLanguagePropertyInfos.Select(p => p.Name);
            var queryProperty = targetProperty
                .Where(item =>
                {
                    foreach (var propertyName in multiLanguagePropertyNames)
                    {
                        var propertyValue = (MultiLanguageString)item.GetType().GetProperty(propertyName)!.GetValue(item)!;
                        foreach(var language in SupportLanguages.List)
                        {
                            if(propertyValue.Get(language) == null)
                            {
                                continue;
                            }

                            if (propertyValue.Get(language)!.Contains(query))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                });

            foreach (var item in queryProperty.Page(pageIndex, pageSize, out pageCount))
            {
                var itemId = (int)item.GetType().GetProperty("Id").GetValue(item);
                var strings = new List<TranslationString>();
                foreach (var propertyInfo in multiLanguagePropertyInfos)
                {
                    var multiLanguageString = propertyInfo.GetValue(item) as MultiLanguageString;
                    var approvingTranslation = _appDbContext.TranslationRecords
                        .Where(_ =>
                            _.Section == section &&
                            _.ItemId == itemId &&
                            _.Property == propertyInfo.Name &&
                            _.Language == CultureInfo.CurrentCulture.Name)
                        .Select(_ => _.Translation)
                        .ToList();
                    strings.Add(new TranslationString(
                        propertyInfo.Name,
                        multiLanguageString.Get(SupportLanguages.Default),
                        multiLanguageString.Get(CultureInfo.CurrentCulture.Name),
                        approvingTranslation));
                }
                translationItemDtos.Add(new TranslationItemDto(itemId, strings));
            }

            ViewData["Section"] = section;
            ViewData["Query"] = query;
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            ViewData["PageCount"] = pageCount;
            ViewData["PageData"] = translationItemDtos;
            return View("Index");
        }
    }
}
