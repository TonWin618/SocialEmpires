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
    }
}
