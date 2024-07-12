using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{

    public class SocialItemDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public SocialItemDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.SocialItems.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<SocialItem, SocialItemDto>("social_items", _appDbContext, _mapper);
        }
    }
}
