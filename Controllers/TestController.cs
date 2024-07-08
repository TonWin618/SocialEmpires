using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Bulletins;

namespace SocialEmpires.Controllers
{
    public class TestController : Controller
    {
        [HttpPost]
        public async Task<Bulletin> MultiLanguageTest([ModelBinder(BinderType = typeof(MultiLanguageJsonModelBinder))] Bulletin bulletin)
        {
            return bulletin;
        }
    }
}
