using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class ExpansionPriceDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public ExpansionPriceDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.ExpansionPrices.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ExpansionPriceDto, ExpansionPrice>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<ExpansionPrice, ExpansionPriceDto>("expansion_prices", _appDbContext, mapper);
        }

        private record ExpansionPriceDto(int Coins, int Neighbors, int Cash);
    }
}
