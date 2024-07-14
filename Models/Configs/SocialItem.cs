using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class SocialItem
    {
        public int Id { get; set; }

        public int WorkerCost {  get; set; }

        public MultiLanguageString Workers {  get; set; }
    }
}
