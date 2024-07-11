using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class Magic
    {
        public int Id { get; set; }
        public MultiLanguageString Name { get; set; }
        public MultiLanguageString Description { get; set; }
        public int Mana { get; set; }
        public string Area { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public int Cash { get; set; }
        public string ImgName { get; set; }
        public int Target { get; set; }
    }
}
