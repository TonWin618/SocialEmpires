using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record ExpansionPriceDto(int Coins, int Neighbors, int Cash);

    public class ExpansionPriceProfile : Profile
    {
        public ExpansionPriceProfile()
        {
            CreateMap<ExpansionPriceDto, ExpansionPrice>().ReverseMap(); ;
        }
    }
}
