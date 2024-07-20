using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Utils;

namespace SocialEmpires.Controllers
{
    public class LanguageController: Controller
    {
        public LanguageController()
        {

        }

        [HttpGet]
        public IActionResult SetLanguage([FromQuery] string language)
        {
            if (!SupportLanguages.Contains(language))
            {
                return NotFound();
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return this.Redirect();
        }
    }
}
