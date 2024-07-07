using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public static class SupportLanguages
    {
        static SupportLanguages()
        {
            List = typeof(MultiLanguageString).GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
                .Select(p => p.Name).ToArray();
        }

        public static string Default => List.First();

        public static string[] List { get; }

        public static bool Contains(string language)
        {
            return List.Contains(language);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ThrowIfUnsupported(string language)
        {
            if (!Contains(language))
            {
                throw new ArgumentException("unsupported language", language);
            }
        }
    }
}
