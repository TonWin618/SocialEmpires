using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Dtos;

namespace SocialEmpires.Seeds
{
    public class LevelRankingRewardDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public LevelRankingRewardDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.LevelRankingRewards.Any())
            {
                return;
            }

            ConfigReadAndSaveUtil.ReadAndSave<LevelRankingReward, LevelRankingRewardDto>("level_ranking_reward", _appDbContext, _mapper);
        }
    }
}
