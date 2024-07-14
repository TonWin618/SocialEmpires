using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record LevelDto(string RewardType, int ExpRequired, MultiLanguageString Name, int RewardAmount);

    public class LevelProfile : Profile
    {
        public static int MappingCount { get; set; }
        public LevelProfile()
        {
            CreateMap<LevelDto, Level>()
                .ForMember(
                    dest => dest.ToLevel,
                    opt => opt.MapFrom(src => MappingCount))
                .AfterMap((src, dest) => MappingCount++)
                .ReverseMap(); 
        }
    }
}
