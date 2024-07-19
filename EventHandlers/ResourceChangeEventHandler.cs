using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using SocialEmpires.Dtos;
using SocialEmpires.Events;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Infrastructures.NotificationHub;
using SocialEmpires.Models.Notifications;

namespace SocialEmpires.EventHandlers
{
    public class ResourceChangeEventHandler : INotificationHandler<ResourceChangeEvent>
    {
        private IHubContext<NotificationHub> _hubContext;
        private IStringLocalizer<SendGiftEventHandler> _localizer;
        private IMapper _mapper;

        public ResourceChangeEventHandler(
            IHubContext<NotificationHub> hubContext,
            IStringLocalizer<SendGiftEventHandler> stringLocalizer,
            IMapper mapper)
        {
            _hubContext = hubContext;
            _localizer = stringLocalizer;
            _mapper = mapper;
        }

        public async Task Handle(ResourceChangeEvent notification, CancellationToken cancellationToken)
        {
            var content = new MultiLanguageString();
            foreach (var language in SupportLanguages.List)
            {
                content.Set(
                    language, 
                    $"[{_localizer["Debug"]}]{_localizer["ResourceChanged"]}:{_localizer[notification.ResourceName]}{notification.Quantity}");
            }

            var notificationEntity = Notification.CreateFromDebug(notification.UserId, content);

            await _hubContext.Clients
                .User(notification.UserId)
                .SendAsync(
                "ReceiveNotification", 
                _mapper.Map<NotificationDto>(notificationEntity));
        }
    }
}
