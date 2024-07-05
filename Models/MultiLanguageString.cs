using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEmpires.Models
{
    [ComplexType]
    public class MultiLanguageString
    {
        public string? En { get; private set; }
        public string? Zh { get; private set; }

        public MultiLanguageString() { }

        public MultiLanguageString(string language, string content) 
        {
            Set(language, content);
        }

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
