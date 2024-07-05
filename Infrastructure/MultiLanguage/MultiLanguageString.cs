using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    [ComplexType]
    public class MultiLanguageString
    {
        [NotMapped]
        [JsonIgnore]
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
                nameof(Zh) => Zh,
                nameof(En) => En,
                //Add more languages...
                _ => null
            };
        }

        public void Set(string language, string? content)
        {
            SupportLanguages.ThrowIfUnsupported(language);

            _ = language switch
            {
                nameof(Zh) => Zh = content,
                nameof(En) => En = content,
                //Add more languages...
                _ => null
            };
        }

        public string? Get()
        {
            return Current == null ? null : Get(Current);
        }

        public void Set(string? content)
        {
            ArgumentNullException.ThrowIfNull(nameof(Current));
            Set(Current!, content);
        }

        public override string ToString()
        {
            return Get() ?? "";
        }
    }
}
