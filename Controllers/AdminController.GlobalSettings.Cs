using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Utils;
using System.Text.Json.Nodes;

namespace SocialEmpires.Controllers
{
    public partial class AdminController
    {
        [HttpGet]
        public IActionResult GlobalSettings()
        {
            ViewData["GlobalSettings"] = _configService.GlobalSettings;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGlobalSetting(string key, string value)
        {
            var setting = await _appDbContext.GlobalSettings.FindAsync(key);
            if (setting != null)
            {
                setting.Value = value;
            }
            return this.Redirect();
        }
    }
}
