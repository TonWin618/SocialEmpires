using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.PlayerSaves
{
    public record class PlayerSave
    {
        public string Pid { get; set; } = null!;

        public PlayerInfo PlayerInfo { get; set; } = null!;

        public List<EmpireMap> Maps { get; set; } = null!;

        public PlayerState PrivateState { get; set; } = null!;

        [JsonIgnore]
        [NotMapped]
        public EmpireMap DefaultMap => Maps.First();

        private PlayerSave()
        {
            // for EF Core
        }

        public static PlayerSave Create(string playerId, string name)
        {
            var info = PlayerInfo.Create(playerId, name);
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
