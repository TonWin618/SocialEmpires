using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class OfferPackDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public OfferPackDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.OfferPacks.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<OfferPack, OfferPackDto>("offer_packs", _appDbContext, _mapper);
        }
    }
}
