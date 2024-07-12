using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Dtos;

namespace SocialEmpires.Seeds
{
    public class MapPriceDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public MapPriceDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.MapPrices.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MapPriceDto, MapPrice>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<MapPrice, MapPriceDto>("map_prices", _appDbContext, mapper);
        }
    }
}
