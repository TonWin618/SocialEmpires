using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Dtos
{
    public record MagicDto(
            int Id,
            MultiLanguageString Name,
            MultiLanguageString Description,
            int Mana,
            string Area,
            int Level,
            int Gold,
            int Cash,
            string ImgName,
            int Target
        );
}
