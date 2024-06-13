using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models
{
    public record class PlayerSave
    {
        public string Pid { get; set; }

        public PlayerInfo PlayerInfo { get; set; }

        public List<EmpireMap> Maps { get; set; }

        public PlayerState PrivateState { get; set; }

        [JsonIgnore]
        [NotMapped]
        public EmpireMap DefaultMap => Maps.First();

        private PlayerSave()
        {
            // for EF Core
        }

        public static PlayerSave Create(string playerId, string name) 
        {
            var info = PlayerInfo.Create(playerId,name);
            var map = EmpireMap.Create(playerId);
            var state = PlayerState.Create(playerId);
            return new PlayerSave()
            {
                Pid = playerId,
                PlayerInfo = info,
                Maps = new() { map },
                PrivateState = state
            };
        }
    }
}
