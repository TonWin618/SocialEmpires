using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Models.Seeds
{
    public class LevelRankingRewardDataSeed
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

        public record LevelRankingRewardDto(
            int Level,
            int Cash,
            Dictionary<string, int> Units
        );
    }
}
