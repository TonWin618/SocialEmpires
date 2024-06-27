using SocialEmpires.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models
{
    public class EmpireMap
    {
        public string Pid { get; set; } = null!;

        public int Id { get; set; }

        public List<int> Expansions { get; set; } = null!;

        public long Timestamp { get; set; }

        public int Coins { get; set; }

        public int Xp { get; set; }

        public int Level { get; set; }

        public int Stone { get; set; }

        public int Wood { get; set; }

        public int Food { get; set; }

        public string Race { get; set; } = null!;

        public int Skin { get; set; }

        public int IdCurrentTreasure { get; set; }

        public long TimestampLastTreasure { get; set; }

        public Dictionary<string, int> ResourcesTraded { get; set; } = null!;

        public Dictionary<string, int> ReceivedAssists { get; set; } = null!;

        public int IncreasedPopulation { get; set; }

        public Dictionary<string, int> ExpirableUnitsTime { get; set; } = null!;

        public List<int> UniversAttackWin { get; set; } = null!;

        public List<long> QuestTimes { get; set; } = null!;

        public List<long> LastQuestTimes { get; set; } = null!;

        [JsonConverter(typeof(MapItemListConverter))]
        public List<MapItem> Items { get; set; } = null!;

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
                ResourcesTraded = [],
                ReceivedAssists = [],
                IncreasedPopulation = 0,
                ExpirableUnitsTime = [],
                UniversAttackWin = [],
                QuestTimes = [],
                LastQuestTimes = [],
                Items =
                [
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
                ]
            };
        }
    }

    public class MapItem : IEquatable<MapItem>
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Orientation { get; set; }
        public long Timestamp { get; set; }
        public int Level { get; set; }
        public List<int>? Units { get; set; }
        public object[]? Attributes { get; set; }

        public MapItem(int id, int x, int y, int orientation, long timestamp, int level, List<int>? units = null, object[]? attributes = null)
        {
            Id = id;
            X = x;
            Y = y;
            Orientation = orientation;
            Timestamp = timestamp;
            Level = level;
            Units = units;
            Attributes = attributes;
        }

        public bool Equals(MapItem? other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id &&
                       X == other.X &&
                       Y == other.Y &&
                       Orientation == other.Orientation &&
                       Timestamp == other.Timestamp &&
                       Level == other.Level &&
                       (Units == other.Units || (Units != null && other.Units != null && Units.SequenceEqual(other.Units))) &&
                       (Attributes == other.Attributes || (Attributes != null && other.Attributes != null && Attributes.SequenceEqual(other.Attributes)));
        }

        public override bool Equals(object? obj) => Equals(obj as MapItem);

        public override int GetHashCode() => (Id, X, Y, Orientation, Timestamp, Level, Units, Attributes).GetHashCode();
    }
}
