namespace SocialEmpires.Models
{
    public class EmpireMap
    {
        public int Id { get; init; }
        public List<int> Expansions { get; init; }
        public long Timestamp { get; init; }
        public int Coins { get; init; }

        public int Xp { get; init; }
        public int Level { get; init; }

        public int Stone { get; init; }
        public int Wood { get; init; }
        public int Food { get; init; }
        public string Race { get; init; }
        public int Skin { get; init; }
        public int IdCurrentTreasure { get; init; }
        public long TimestampLastTreasure { get; init; }
        public Dictionary<string, int> ResourcesTraded { get; init; }
        public Dictionary<string, int> ReceivedAssists { get; init; }
        public int IncreasedPopulation { get; init; }
        public Dictionary<string, long> ExpirableUnitsTime { get; init; }
        public List<int> UniversAttackWin { get; init; }
        public List<long> QuestTimes { get; init; }
        public List<long> LastQuestTimes { get; init; }
        public List<(int, int, int, int, int, int, int[], int[])> Items { get; init; }
    }
}
