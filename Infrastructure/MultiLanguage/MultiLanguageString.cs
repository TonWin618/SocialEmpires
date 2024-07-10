using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    [ComplexType]
    public class MultiLanguageString
    {
        [NotMapped]
        [JsonIgnore]
        public string? Current { get; init; }

#pragma warning disable IDE1006 // Naming Styles
        public string? en { get; private set; }
        public string? zh { get; private set; }
        //Add more languages...
#pragma warning restore IDE1006 // Naming Styles

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
                nameof(zh) => zh,
                nameof(en) => en,
                //Add more languages...
                _ => null
            };
        }

        public void Set(string language, string? content)
        {
            SupportLanguages.ThrowIfUnsupported(language);

            _ = language switch
            {
                nameof(zh) => zh = content,
                nameof(en) => en = content,
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

        public static implicit operator string(MultiLanguageString source)
            => source.ToString();

        public static implicit operator MultiLanguageString(string source)
            => ConvertFromString(source);

        public override string ToString()
        {
            return Get() ?? "";
        }

        public static MultiLanguageString ConvertFromString(string source)
        {
            return new MultiLanguageString(CultureInfo.CurrentCulture.Name, source);
        }
    }
}
