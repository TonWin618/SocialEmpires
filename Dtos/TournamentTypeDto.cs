using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json.Serialization;

namespace SocialEmpires.Dtos
{
    public class TournamentTypeDto
    {
        public int Id { get; set; }
        public MultiLanguageString Name { get; set; }
        public string ResourceType { get; set; }
        public int Cost { get; set; }
        public List<TournamentPrizeDto> Prize { get; set; }
        public int NumPlayers { get; set; }
        public int Duration { get; set; }
        public string Picture { get; set; }
        public int MinLevel { get; set; }
        public long MapId { get; set; }
        public int WeeklyTournaments { get; set; }
        public List<TournamentOpponentDto>? WeeklyOpponent { get; set; }
    }

    public class TournamentPrizeDto
    {
        public int G { get; set; }
        //[JsonConverter(typeof(StringIntDictionaryConverter))]
        public Dictionary<string, int> U { get; set; }
    }

    public class TournamentOpponentDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public int Xp { get; set; }
        public int Level { get; set; }
        public string Country { get; set; }
        public List<int> Team { get; set; }
    }

    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<TournamentType, TournamentTypeDto>().ReverseMap();
            CreateMap<TournamentPrize, TournamentPrizeDto>().ReverseMap();
            CreateMap<TournamentOpponent, TournamentOpponentDto>().ReverseMap();
        }
    }
}
