using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Dtos
{
    public record NeighborAssistDto(
            Reward Reward,
            int Rnd,
            MultiLanguageString Task,
            MultiLanguageString Action,
            MultiLanguageString Notification);
}
