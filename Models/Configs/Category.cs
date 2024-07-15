using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class Category
    {
        public int Id { get; set; }

        public MultiLanguageString Name {  get; set; }

        public List<SubCategory> Sub { get; set; }
    }

    public class SubCategory
    {
        public int Id { get; set; }

        public MultiLanguageString Name { get; set; }

        public int Parent {  get; set; }
    }
}
