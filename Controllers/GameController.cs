using Microsoft.AspNetCore.Mvc;

namespace SocialEmpires.Controllers
{
    public class GameController : ControllerBase
    {
        [HttpGet("crossdomain.xml")]
        public ActionResult GetCrossdomainXmlFile()
        {
            return SendFromLocal("crossdomain.xml", "application/xml");
        }


        private PhysicalFileResult SendFromLocal(string relativePath, string contentType)
        {
            return PhysicalFile(
                Directory.GetCurrentDirectory() + "/Assets/" + relativePath, 
                contentType);
        }
    }
}
