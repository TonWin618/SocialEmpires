using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class Chore
    {
        public int Id {  get; set; }
        public MultiLanguageString Categories {  get; set; }
        public string Images {  get; set; }
        public string Globals {  get; set; }
        public string UnitsCollectionsCategories {  get; set; }
        public string TournamentType {  get; set; }
    }
}
