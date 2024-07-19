using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialEmpires.Dtos;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Notifications;
using System.Globalization;
using System.Text.Json;
using SocialEmpires.Utils;

namespace SocialEmpires.Controllers
{
    public partial class ManagerController
    {
        [HttpGet]
        public IActionResult Bulletin()
        {
            var bulletins = _appDbContext.Notifications
                .Where(_ => _.ExpiryTime > DateTime.UtcNow && _.Type == NotificationTypeNames.Bulletin)
                .WithLanguage(CultureInfo.CurrentCulture.Name)
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
            var bulletin = Notification.CreateFromBulletin(
                UserId, 
                new MultiLanguageString(language, htmlContent), 
                new TimeSpan(days, hours, minutes, seconds));

            await _appDbContext.AddAsync(bulletin);
            var bulletinDto = _mapper.Map<NotificationDto>(bulletin);
            await _bulletinHubContext.Clients.All.SendAsync("ReceiveBulletin", JsonSerializer.Serialize(bulletinDto));
            return this.Redirect();
        }
    }
}
