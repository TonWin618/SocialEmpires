﻿using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Models.Configs
{
    public class LocalizationString
    {
        public int Id {  get; set; }

        public MultiLanguageString Name { get; set; }

        public MultiLanguageString Text {  get; set; }
    }
}
