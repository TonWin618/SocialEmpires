using System.Globalization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public static class SupportLanguages
    {
        public const string Zh = "Zh";
        public const string En = "En";
        //Add more languages...

        public static string[] List => [Zh, En];//Add more languages...

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

        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToUpper(input[0], CultureInfo.InvariantCulture) + input.Substring(1);
        }
    }
}
