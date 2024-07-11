using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Configs
{
    public class LevelRankingReward
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int Cash { get; set; }
        public Dictionary<string, int> Units { get; set; }
    }
}
