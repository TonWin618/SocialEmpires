using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEmpires.Models
{
    [ComplexType]
    public class MultiLanguageString
    {
        public string? En { get; set; }
        public string? Zh { get; set; }

        public string Get(string language)
        {
            return language switch
            {
                "zh" => Zh,
                "en" => En,
                _ => null
            }?? "";
        }

        public void Set(string language, string content)
        {
            _ = language switch
            {
                "zh" => Zh = content,
                "en" => En = content,
                _ => null
            };
        }
    }
}
