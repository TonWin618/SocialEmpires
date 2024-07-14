using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record FindableItemDto(int Id, MultiLanguageString Title, MultiLanguageString Description, int Coins);

    public class FindableItemProfile : Profile
    {
        public FindableItemProfile()
        {
            CreateMap<FindableItemDto, FindableItem>().ReverseMap(); ;
        }
    }
}
