using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialEmpires.Models.Bulletins;
using System.Text.Json;

namespace SocialEmpires.Controllers
{
    public partial class AdminController
    {
        [HttpGet]
        public IActionResult Bulletin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PublishBulletin(
            [FromForm] string htmlContent,
            [FromForm] int days,
            [FromForm] int hours, 
            [FromForm] int minutes, 
            [FromForm] int seconds)
        {
            var bulletin = new Bulletin(UserId, htmlContent, Request.Headers.AcceptLanguage, new TimeSpan(days, hours, minutes, seconds));
            await _bulletinHubContext.Clients.All.SendAsync("ReceiveBulletin", JsonSerializer.Serialize(bulletin)); 
            return Redirect(Request.Headers.Referer);
        }
    }
}
