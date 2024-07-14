using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Dtos;

namespace SocialEmpires.Seeds
{
    public class TownPriceDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TownPriceDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.TownPrices.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<TownPrice, TownPriceDto>("town_prices", _appDbContext, _mapper);
        }
    }
}
