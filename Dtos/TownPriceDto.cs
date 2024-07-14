using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record TownPriceDto(int Coins, int Cash, int Level);

    public class TownPriceProfile : Profile
    {
        public TownPriceProfile()
        {
            CreateMap<TownPriceDto, TownPrice>().ReverseMap(); ;
        }
    }
}
