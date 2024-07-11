using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class Mission
    {
        public int Id { get; init; }
        public MultiLanguageString Description { get; init; } = null!;
        public string Hint { get; init; } = null!;
        public int Reward { get; init; }
        public MultiLanguageString Title { get; init; } = null!;
    }
}
