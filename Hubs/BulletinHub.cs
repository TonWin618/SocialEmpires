using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace SocialEmpires.Hubs
{
    public class BulletinHub: Hub
    {
        public async Task PublishTopBulletin(List<BulletinContentSegement> content)
        {
            var bulletin = new Bulletin(Context.UserIdentifier, content);
            await Clients.All.SendAsync("ReceiveTopBulletin", JsonSerializer.Serialize(bulletin));
        }

        public async Task PublishColorfulBulletin(List<BulletinContentSegement> content)
        {
            var bulletin = new Bulletin(Context.UserIdentifier, content);
            await Clients.All.SendAsync("ReceiveBulletin", JsonSerializer.Serialize(bulletin));
        }

        public async Task PublishBulletin(string content)
        {
            var bulletin = new Bulletin(Context.UserIdentifier, content);
            await Clients.All.SendAsync("ReceiveBulletin", JsonSerializer.Serialize(bulletin));
        }

        public class Bulletin
        {
            public string PublisherId { get; set; }

            public DateTime PublishedTime { get; set; }

            public List<BulletinContentSegement> Content { get; set; }

            public Bulletin(string publisherId, string content)
            {
                PublisherId = publisherId;
                PublishedTime = DateTime.UtcNow;
                Content = [(new BulletinContentSegement(content, "black"))];
            }
            
            public Bulletin(string publisherId, List<BulletinContentSegement>? content = null)
            {
                PublisherId = publisherId;
                PublishedTime = DateTime.UtcNow;
                Content = content ?? []; 
            }

            public Bulletin Append(string text, string color)
            {
                var segement = new BulletinContentSegement(text, color);
                Content.Add(segement);
                return this;
            }
        }

        public record BulletinContentSegement(string Text, string Color);
    }
}
