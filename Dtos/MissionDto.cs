using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Dtos
{
    public record MissionDto(MultiLanguageString Description, string Hint, int Reward, int Id, MultiLanguageString Title);
}
