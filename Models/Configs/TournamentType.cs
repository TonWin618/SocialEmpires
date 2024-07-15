using SocialEmpires.Infrastructure.MultiLanguage;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEmpires.Models.Configs
{
    public class TournamentType
    {
        public int Id { get; set; }
        public MultiLanguageString Name { get; set; }
        public string ResourceType { get; set; }
        public int Cost { get; set; }
        public List<TournamentPrize> Prize { get; set; }
        public int NumPlayers { get; set; }
        public int Duration { get; set; }
        public string Picture { get; set; }
        public int MinLevel { get; set; }
        public long MapId { get; set; }
        public int WeeklyTournaments { get; set; }
        public List<TournamentOpponent> WeeklyOpponent { get; set; }
    }

    public class TournamentPrize
    {
        public int Id { get; set; }
        public int G { get; set; }
        public Dictionary<string, int> U { get; set; }
    }

    public class TournamentOpponent
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public int Xp { get; set; }
        public int Level { get; set; }
        public string Country { get; set; }
        public List<int> Team { get; set; }
    }
}
