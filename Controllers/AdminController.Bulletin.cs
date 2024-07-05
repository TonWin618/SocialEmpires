using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Bulletins;
using System.Text.Json;

namespace SocialEmpires.Controllers
{
    public partial class AdminController
    {
        [HttpGet]
        public IActionResult Bulletin()
        {
            var bulletins = _appDbContext.Bulletins
                .WithLanguage(RequestCultrue)
                .Where(_ => _.ExpiryTime > DateTime.UtcNow)
                .OrderBy(_ => _.PublishedTime)
                .ToList();

            ViewData["Bulletins"] = bulletins;
             
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PublishBulletin(
            [FromForm] string htmlContent,
            [FromForm] string language,
            [FromForm] int days,
            [FromForm] int hours, 
            [FromForm] int minutes, 
            [FromForm] int seconds)
        {
            var bulletin = new Bulletin(UserId, htmlContent, language, new TimeSpan(days, hours, minutes, seconds));
            await _appDbContext.AddAsync(bulletin);
            await _bulletinHubContext.Clients.All.SendAsync("ReceiveBulletin", JsonSerializer.Serialize(bulletin)); 
            return Redirect(Request.Headers.Referer);
        }
    }
}
