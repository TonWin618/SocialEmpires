using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Dtos
{
    public record HonorLevelDto(
            int Id,
            int Points,
            MultiLanguageString Rank
        );
}
