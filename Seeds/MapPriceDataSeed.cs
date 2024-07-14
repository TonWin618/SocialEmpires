using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Dtos;

namespace SocialEmpires.Seeds
{
    public class MapPriceDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public MapPriceDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.MapPrices.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<MapPrice, MapPriceDto>("map_prices", _appDbContext, _mapper);
        }
    }
}
