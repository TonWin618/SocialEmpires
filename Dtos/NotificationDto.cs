﻿using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Notifications;

namespace SocialEmpires.Dtos
{
    public class NotificationDto
    {
        public DateTime PublishedTime { get; private set; }

        public DateTime ExpiryTime { get; private set; }

        public string Type { get; private set; } = null!;

        public MultiLanguageString HtmlContent { get; private set; } = null!;
    }

    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
