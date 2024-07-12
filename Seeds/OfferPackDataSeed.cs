using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class OfferPackDataSeed : IDataSeed
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
    }
}
