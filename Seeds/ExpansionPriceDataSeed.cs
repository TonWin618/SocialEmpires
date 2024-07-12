using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Dtos;

namespace SocialEmpires.Seeds
{
    public class ExpansionPriceDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ExpansionPriceDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.ExpansionPrices.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<ExpansionPrice, ExpansionPriceDto>("expansion_prices", _appDbContext, _mapper);
        }
    }
}
