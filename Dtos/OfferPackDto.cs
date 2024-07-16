using AutoMapper;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json.Serialization;

namespace SocialEmpires.Dtos
{
    public record OfferPackDto(
            int Position,
            int CostCash,
            int Gold,
            int Stone,
            int Food,
            int Wood,
            int Xp,
            int Mana,
            int Enabled,
            int PackType
        )
    {
        [JsonConverter(typeof(IntListOrIntListListConverter))]
        public List<object> Items { get; set; } = null!;
    }

    public class OfferPackProfile : Profile
    {
        public OfferPackProfile()
        {
            CreateMap<OfferPackDto, OfferPack>().ReverseMap();
        }
    }
}
