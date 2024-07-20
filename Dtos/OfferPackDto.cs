using AutoMapper;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json.Serialization;

namespace SocialEmpires.Dtos
{
    public record OfferPackDto(
            int? Id,
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
            CreateMap<OfferPack, OfferPackDto>()
                .ForCtorParam(nameof(OfferPackDto.Enabled), opt => opt.MapFrom(src => src.Enabled == true ? 1 : 0));
            CreateMap<OfferPackDto, OfferPack>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int?)null));
        }
    }
}
