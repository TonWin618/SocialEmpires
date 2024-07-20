﻿using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record DartsItemDto(
            string StartDate,
            List<int> Items,
            int ExtraItem
        );

    public class DartsItemProfile: Profile
    {
        public DartsItemProfile() 
        {
            CreateMap<DartsItemDto, DartsItem>().ReverseMap(); ;
        }
    }
}
