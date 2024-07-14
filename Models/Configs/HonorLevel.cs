using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class HonorLevel
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public MultiLanguageString Rank { get; set; }
    }
}
