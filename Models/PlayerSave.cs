using System.Text.Json.Serialization;

namespace SocialEmpires.Models
{
    public record class PlayerSave
    {
        public PlayerInfo PlayerInfo { get; set; }

        public List<EmpireMap> Maps { get; set; }

        public PlayerState PrivateState { get; set; }

        [JsonIgnore]
        public EmpireMap DefaultMap => Maps.FirstOrDefault(_ => _.Id == PlayerInfo.DefaultMap);
    }
}
