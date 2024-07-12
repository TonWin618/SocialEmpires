using AutoMapper;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Dtos;

namespace SocialEmpires.Seeds
{
    public class LevelRankingRewardDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public LevelRankingRewardDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.LevelRankingRewards.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LevelRankingRewardDto, LevelRankingReward>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<LevelRankingReward, LevelRankingRewardDto>("level_ranking_reward", _appDbContext, mapper);
        }
    }
}
