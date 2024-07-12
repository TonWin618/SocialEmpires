using AutoMapper;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record LevelRankingRewardDto(
            int Level,
            int Cash,
            Dictionary<string, int> Units
        );

    public class LevelRankingRewardProfile : Profile
    {
        public LevelRankingRewardProfile()
        {
            CreateMap<LevelRankingRewardDto, LevelRankingReward>().ReverseMap(); ;
        }
    }
}
