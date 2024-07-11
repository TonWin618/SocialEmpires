using SocialEmpires.Infrastructure.MultiLanguage;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEmpires.Models.Configs
{
    public class NeighborAssist
    {
        public int Id {  get; set; }

        public Reward Reward { get; set; }

        public int Rnd {  get; set; }

        public MultiLanguageString Task { get; set; }

        public MultiLanguageString Action { get; set; }

        public MultiLanguageString Notification { get; set; }
    }

    [ComplexType]
    public class Reward
    {
        public int Coins { get; set; }

        public int Cash { get; set; }

        public int Xp { get; set; }
    }
}
