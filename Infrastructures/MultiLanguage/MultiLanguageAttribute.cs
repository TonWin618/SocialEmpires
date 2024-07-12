using Microsoft.AspNetCore.Mvc;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public class MultiLanguageAttribute: ModelBinderAttribute
    {
        public MultiLanguageAttribute():base(typeof(MultiLanguageJsonModelBinder))
        {

        }
    }
}
