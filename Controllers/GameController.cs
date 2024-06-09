using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SocialEmpires.Controllers
{
    public class GameController : ControllerBase
    {
        [HttpGet("crossdomain.xml")]
        public ActionResult GetCrossdomainXml()
        {
            return SendFromLocal("crossdomain.xml", "application/xml");
        }

        
        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/{*path}")]
        public ActionResult GetStaticAssets(string path)
        {
            return SendFromLocal(path, "application/octet-stream");
        }

        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/swf/05122012_projectiles.swf")]
        public ActionResult GetProjectiles()
        {
            return SendFromLocal("swf/05122012_projectiles.swf", "application/octet-stream");
        }

        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/swf/05122012_magicParticles.swf")]
        public ActionResult GetMagicParticles()
        {
            return SendFromLocal("swf/05122012_magicParticles.swf", "application/octet-stream");
        }

        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/swf/05122012_dynamic.swf")]
        public ActionResult GetDynamic()
        {
            return SendFromLocal("swf/05122012_dynamic.swf", "application/octet-stream");
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/track_game_status.php")]
        public ActionResult TrackGameStatus()
        {
            return Ok("");
        }

        //[HttpGet("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/get_game_config.php")]

        //[HttpGet("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/get_player_info.php")]

        //[HttpGet("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/sync_error_track.php")]

        private PhysicalFileResult SendFromLocal(string relativePath, string contentType)
        {
            return PhysicalFile(
                Directory.GetCurrentDirectory() + "/Assets/" + relativePath, 
                contentType);
        }
    }
}
