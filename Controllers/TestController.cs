﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Bulletins;

namespace SocialEmpires.Controllers
{
    [Authorize("Admin")]
    public class TestController : Controller
    {
        [HttpPost]
        public IActionResult MultiLanguageTest([MultiLanguage] Bulletin bulletin)
        {
            return this.JsonWithLanguage(bulletin);
        }
    }
}
