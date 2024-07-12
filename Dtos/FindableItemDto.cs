using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Dtos
{
    public record FindableItemDto(int Id, MultiLanguageString Title, MultiLanguageString Description, int Coins);
}
