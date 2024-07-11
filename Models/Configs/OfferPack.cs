using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialEmpires.Utils;
using System.Text.Json.Serialization;

namespace SocialEmpires.Models.Configs
{
    public class OfferPack
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int CostCash { get; set; }
        public int Gold { get; set; }
        public int Stone { get; set; }
        public int Food { get; set; }
        public int Wood { get; set; }
        public int Xp { get; set; }
        public List<int> Items { get; set; }
        public int Mana { get; set; }
        public bool Enabled { get; set; }
        public int PackType { get; set; }
    }

}
