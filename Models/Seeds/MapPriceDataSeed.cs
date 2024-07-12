using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
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

        public record MapPriceDto(
            int Coins,
            int Cash,
            int Level
        );
    }
}
