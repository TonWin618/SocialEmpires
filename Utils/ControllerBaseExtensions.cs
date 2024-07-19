using Microsoft.AspNetCore.Mvc;

namespace SocialEmpires.Utils
{
    public static class ControllerBaseExtensions
    {
        public static RedirectResult Redirect(this ControllerBase controller)
        {
            return controller.Redirect(controller.Request.Headers.Referer.ToString() ?? "/");
        }
    }
}
