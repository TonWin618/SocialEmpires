using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    [JsonConverter(typeof(MultiLanguageStringConverter))]
    [ComplexType]
    public class MultiLanguageString
    {
        [NotMapped]
        public string? Current { get; init; }

        public string? En { get; private set; }
        public string? Zh { get; private set; }
        //Add more languages...

        public MultiLanguageString() { }

        public MultiLanguageString(string language, string? content)
        {
            Set(language, content);
        }

        public string? Get(string language)
        {
            SupportLanguages.ThrowIfUnsupported(language);

            return language switch
            {
                SupportLanguages.Zh => Zh,
                SupportLanguages.En => En,
                //Add more languages...
                _ => null
            };
        }

        public string? GetCurrent()
        {
            return Current switch
            {
                SupportLanguages.Zh => Zh,
                SupportLanguages.En => En,
                //Add more languages...
                _ => null
            };
        }

        public void Set(string language, string? content)
        {
            SupportLanguages.ThrowIfUnsupported(language);

            _ = language switch
            {
                SupportLanguages.Zh => Zh = content,
                SupportLanguages.En => En = content,
                //Add more languages...
                _ => null
            };
        }

        public void SetCurrent(string? content)
        {
            ArgumentNullException.ThrowIfNull(nameof(Current));
            Set(Current!, content);
        }
    }
}
