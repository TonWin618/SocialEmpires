namespace SocialEmpires.Models
{
    public class EmpireMap
    {
        public string Pid { get; set; }

        public List<int> Expansions { get; set; }

        public long Timestamp { get; set; }

        public int Coins { get; set; }

        public int Xp { get; set; }

        public int Level { get; set; }

        public int Stone { get; set; }

        public int Wood { get; set; }

        public int Food { get; set; }

        public string Race { get; set; }

        public int Skin { get; set; }

        public int IdCurrentTreasure { get; set; }

        public long TimestampLastTreasure { get; set; }

        public Dictionary<string, int> ResourcesTraded { get; set; }

        public Dictionary<string, int> ReceivedAssists { get; set; }

        public int IncreasedPopulation { get; set; }

        public Dictionary<string, int> ExpirableUnitsTime { get; set; }

        public List<int> UniversAttackWin { get; set; }

        public List<long> QuestTimes { get; set; }

        public List<long> LastQuestTimes { get; set; }

        public List<MapItem> Items { get; set; }
    }

    public record MapItem(int Id, int Row, int Col, int Keep1, long Time, int Keep2, int[] Contents, object[] Objects)
    {

    };
}
