using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace SocialEmpires.Controllers
{
    public partial class AdminController
    {
        [HttpGet]
        public IActionResult GlobalSettings()
        {
            ViewData["GlobalSettings"] = JsonNode.Parse(_configFileService.Globals.ToString());
            return View();
        }
    }
}
