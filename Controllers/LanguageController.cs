﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace SocialEmpires.Controllers
{
    public class LanguageController: Controller
    {
        public LanguageController()
        {

        }

        [HttpGet]
        public IActionResult SetLanguage([FromQuery] string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return Redirect(Request.Headers.Referer);
        }
    }
}
