namespace SocialEmpires.Models.Configs
{
    public class Level
    {
        public string RewardType { get; set; } = null!;
        public int ExpRequired { get; set; }
        public string Name { get; set; } = null!;
        public int RewardAmount { get; set; }
    }
}
