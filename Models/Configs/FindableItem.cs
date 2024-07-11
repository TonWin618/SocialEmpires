using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class FindableItem
    {
        public int Id { get; set; }
        public MultiLanguageString Title { get; set; } = null!;
        public MultiLanguageString Description { get; set; } = null!;
        public int Coins { get; set; }
    }
}
