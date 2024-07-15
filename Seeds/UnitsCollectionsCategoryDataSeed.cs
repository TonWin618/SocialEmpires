using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models;

namespace SocialEmpires.Seeds
{
    public class UnitsCollectionsCategoryDataSeed: IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UnitsCollectionsCategoryDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.UnitsCollectionsCategories.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSaveObject<UnitsCollectionsCategory, UnitsCollectionsCategoryDto>("units_collections_categories", _appDbContext, _mapper);
        }
    }
}
