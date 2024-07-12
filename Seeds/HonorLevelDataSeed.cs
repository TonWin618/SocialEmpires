using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class HonorLevelDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public HonorLevelDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.HonorLevels.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<HonorLevel, HonorLevelDto>("honor_levels", _appDbContext, _mapper);
        }
    }
}
