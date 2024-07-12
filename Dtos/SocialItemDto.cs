using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Dtos
{
    public record SocialItemDto(int Id, int WorkerCost, MultiLanguageString Workers);
}
