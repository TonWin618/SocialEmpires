namespace SocialEmpires.Dtos
{
    public record LevelRankingRewardDto(
            int Level,
            int Cash,
            Dictionary<string, int> Units
        );
}
