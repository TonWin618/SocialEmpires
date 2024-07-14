using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class Level
    {
        public int Id { get; set; }
        public int ToLevel { get; set; }
        public string RewardType { get; set; } = null!;
        public int ExpRequired { get; set; }
        public MultiLanguageString Name { get; set; } = null!;
        public int RewardAmount { get; set; }
    }
}
