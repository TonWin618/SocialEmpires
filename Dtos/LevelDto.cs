using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Dtos
{
    public record LevelDto(string RewardType, int ExpRequired, MultiLanguageString Name, int RewardAmount);
}
