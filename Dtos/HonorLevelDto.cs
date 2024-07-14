using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record HonorLevelDto(
            int Id,
            int Points,
            MultiLanguageString Rank
        );

    public class HonorLevelProfile : Profile
    {
        public HonorLevelProfile()
        {
            CreateMap<HonorLevelDto, HonorLevel>().ReverseMap(); ;
        }
    }
}
