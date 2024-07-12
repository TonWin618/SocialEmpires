using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record MapPriceDto(
            int Coins,
            int Cash,
            int Level
        );

    public class MapPriceProfile : Profile
    {
        public MapPriceProfile()
        {
            CreateMap<MapPriceDto, MapPrice>().ReverseMap(); ;
        }
    }
}
