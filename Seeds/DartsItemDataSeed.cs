using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Dtos;

namespace SocialEmpires.Seeds
{
    public class DartsItemDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public DartsItemDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.DartsItems.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<DartsItem, DartsItemDto>("darts_items", _appDbContext, _mapper);
        }
    }
}
