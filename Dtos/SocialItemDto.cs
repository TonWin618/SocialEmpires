using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record SocialItemDto(int Id, int WorkerCost, MultiLanguageString Workers);

    public class SocialItemProfile : Profile
    {
        public SocialItemProfile()
        {
            CreateMap<SocialItemDto, SocialItem>().ReverseMap(); ;
        }
    }
}
