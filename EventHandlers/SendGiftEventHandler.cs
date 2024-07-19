using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using SocialEmpires.Events;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Infrastructures.NotificationHub;
using SocialEmpires.Models;
using SocialEmpires.Models.Notifications;
using SocialEmpires.Services;
using System.Globalization;
using System.Text;

namespace SocialEmpires.EventHandlers
{
    public class SendGiftEventHandler : INotificationHandler<SendGiftEvent>
    {
        private AppDbContext _appDbContext;
        private IHubContext<NotificationHub> _hubContext;
        private IStringLocalizer<SendGiftEventHandler> _stringLocalizer;
        private ConfigService _configService;

        public SendGiftEventHandler(
            AppDbContext appDbContext,
            IHubContext<NotificationHub> hubContext,
            IStringLocalizer<SendGiftEventHandler> stringLocalizer,
            ConfigService configService)
        {
            _appDbContext = appDbContext;
            _hubContext = hubContext;
            _stringLocalizer = stringLocalizer;
            _configService = configService;
        }

        public async Task Handle(SendGiftEvent notification, CancellationToken cancellationToken)
        {
            var content = new MultiLanguageString();
            foreach(var language in SupportLanguages.List)
            {
                content.Set(language, BuildContent(notification, language));
            }
            _appDbContext.Add(Notification.CreateFromGift(notification.PublisherId, notification.UserIds, content, TimeSpan.FromDays(7)));
        }

        private string BuildContent(SendGiftEvent notification, string language)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(language);
            var sb = new StringBuilder();
            sb.AppendLine(_stringLocalizer["GiftPack"]);

            if (notification.Cash != 0)
            {
                sb.Append($"{_stringLocalizer["Cash"]}*{notification.Cash}, ");
            }
            if (notification.Gold != 0)
            {
                sb.Append($"{_stringLocalizer["Gold"]}*{notification.Gold}, ");
            }
            if (notification.Wood != 0)
            {
                sb.Append($"{_stringLocalizer["Wood"]}*{notification.Wood}, ");
            }
            if (notification.Food != 0)
            {
                sb.Append($"{_stringLocalizer["Food"]}*{notification.Food}, ");
            }
            if (notification.Stone != 0)
            {
                sb.Append($"{_stringLocalizer["Stone"]}*{notification.Stone}, ");
            }
            if (notification.Xp != 0)
            {
                sb.Append($"{_stringLocalizer["Xp"]}*{notification.Xp}, ");
            }
            sb.AppendLine();
            var itemGroups = notification.items.GroupBy(_ => _);
            foreach (var itemGroup in itemGroups)
            {
                var itemName = _configService.Items.Find(_ => _.Id == itemGroup.Key)!.Name.Get(language);
                sb.Append($"{itemName}*{itemGroup.Count()}, ");
            }
            return sb.ToString();
        }
    }
}
