using SocialEmpires.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models
{
    public class EmpireMap
    {
        public string Pid { get; set; }

        public int Id {  get; set; }

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

        [JsonConverter(typeof(MapItemListConverter))]
        public List<MapItem> Items { get; set; }

        [NotMapped]
        [JsonInclude]
        [JsonPropertyName("__#__ITEMS_hint")]
        public string[] ItemsHint = ["item_id", "x", "y", "orientation", "collected_at_timestamp", "level", "units_array []", "attributes_dict {}"];

        private EmpireMap()
        {
            // for EF Core
        }

        public static EmpireMap Create(string playerId)
        {
            return new()
            {
                Pid = playerId,
                Id = 0,
                Expansions = [13],
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Coins = 250,
                Xp = 4,
                Level = 1,
                Stone = 250,
                Wood = 850,
                Food = 700,
                Race = "h",
                Skin = 0,
                IdCurrentTreasure = 0,
                TimestampLastTreasure = 0,
                ResourcesTraded = new(),
                ReceivedAssists = new(),
                IncreasedPopulation = 0,
                ExpirableUnitsTime = new(),
                UniversAttackWin = new(),
                QuestTimes = new(),
                LastQuestTimes = new(),
                Items = new()
                {
                    new(26,52,52,0,0,0,[],[]),
                    new(19,50,58,0,0,0,[],[]),
                    new(512,50,44,2,0,0,[],[]),
                    new(512,50,42,0,0,0,[],[]),
                    new(516,51,44,1,0,0,[],[]),
                    new(516,51,42,0,0,0,[],[]),
                    new(1,44,50,0,0,0,[],[]),
                    new(1,44,52,0,0,0,[],[]),
                    new(19,45,50,0,0,0,[],[]),
                    new(19,45,51,0,0,0,[],[]),
                    new(19,45,52,0,0,0,[],[]),
                    new(19,45,53,0,0,0,[],[]),
                    new(19,45,54,0,0,0,[],[]),
                    new(19,44,54,0,0,0,[],[]),
                    new(19,43,54,0,0,0,[],[]),
                    new(29,51,47,0,0,0,[],[]),
                    new(29,59,49,0,0,0,[],[]),
                    new(525,40,40,0,0,0,[],[]),
                    new(525,39,40,1,0,0,[],[])
                }
            };
        }
    }

    public record MapItem(
        int Id, 
        int X, 
        int Y, 
        int Orientation, 
        long Timestamp, 
        int Level, 
        int[]? Units = null, 
        object[]? Attributes = null)
    {

    };
}
