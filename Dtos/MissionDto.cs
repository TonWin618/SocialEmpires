using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record MissionDto(MultiLanguageString Description, string Hint, int Reward, int Id, MultiLanguageString Title);

    public class MissionProfile : Profile
    {
        public MissionProfile()
        {
            CreateMap<MissionDto, Mission>().ReverseMap(); ;
        }
    }
}
