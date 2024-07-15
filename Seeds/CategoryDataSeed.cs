using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models;

namespace SocialEmpires.Seeds
{
    public class CategoryDataSeed: IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CategoryDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.Categories.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSaveObject<Category, CategoryDto>("categories", _appDbContext, _mapper);
        }
    }
}
