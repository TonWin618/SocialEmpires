using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class TownPriceDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public TownPriceDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.TownPrices.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TownPriceDto, TownPrice>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<TownPrice, TownPriceDto>("town_prices", _appDbContext, mapper);
        }

        public record TownPriceDto(
            int Coins,
            int Cash,
            int Level
        );
    }
}
