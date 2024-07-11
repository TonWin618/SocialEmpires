using AutoMapper;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Seeds
{
    public class OfferPackDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public OfferPackDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.OfferPacks.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OfferPackDto, OfferPack>()
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled == 1));
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<OfferPack, OfferPackDto>("offer_packs", _appDbContext, mapper);
        }

        public record OfferPackDto(
            int Id,
            int Position,
            int CostCash,
            int Gold,
            int Stone,
            int Food,
            int Wood,
            int Xp,
            List<int> Items,
            int Mana,
            int Enabled,
            int PackType
        );
    }
}
