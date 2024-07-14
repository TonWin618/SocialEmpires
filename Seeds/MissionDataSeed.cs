using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class MissionDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public MissionDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.Missions.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<Mission, MissionDto>("missions", _appDbContext, _mapper);
        }
    }
}
