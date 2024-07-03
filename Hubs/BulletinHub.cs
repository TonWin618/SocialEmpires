using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocialEmpires.Models.Bulletins;
using System.Text.Json;

namespace SocialEmpires.Hubs
{
    public class BulletinHub: Hub
    {
        [Authorize(Roles = "Admin")]
        public async Task PublishBulletin(string htmlContent, string type, TimeSpan expiryTimeSpan)
        {
            var bulletin = new Bulletin(Context.UserIdentifier, htmlContent, expiryTimeSpan, type);
            await Clients.All.SendAsync("ReceiveBulletin", JsonSerializer.Serialize(bulletin));
        }
    }
}
