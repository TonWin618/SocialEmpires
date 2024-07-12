using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Dtos
{
    public record LocalizationStringDto(int Id, MultiLanguageString Name, MultiLanguageString Text);
}
