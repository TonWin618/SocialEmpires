using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record NeighborAssistDto(
            Reward Reward,
            int Rnd,
            MultiLanguageString Task,
            MultiLanguageString Action,
            MultiLanguageString Notification);

    public class NeighborAssistProfile : Profile
    {
        public NeighborAssistProfile()
        {
            CreateMap<NeighborAssistDto, NeighborAssist>().ReverseMap(); ;
        }
    }
}
