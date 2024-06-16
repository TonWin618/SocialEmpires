using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Services;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles ="User")]
    public class GameController : ControllerBase
    {
        private readonly PlayerSaveService _playerSaveService;

        public GameController(
            PlayerSaveService playerSaveService) 
        {
            _playerSaveService = playerSaveService;
        }

        [HttpGet("CreatePlayerSaveForTest")]
        public async Task<ActionResult> CreatePlayerSaveForTest()
        {
            var id = Guid.NewGuid().ToString();
            var user = await _playerSaveService.CreatePlayerSaveAsync(id, id);

            return Ok(id);
        }

        [HttpGet("crossdomain.xml")]
        public ActionResult GetCrossdomainXml()
        {
            return SendFromLocal("crossdomain.xml", "application/xml");
        }
        
        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/{*path}")]
        public ActionResult GetStaticAssets(string path)
        {
            return SendFromLocal(path, "application/x-shockwave-falsh");
        }

        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/swf/05122012_projectiles.swf")]
        public ActionResult GetProjectiles()
        {
            return SendFromLocal("swf/05122012_projectiles.swf", "application/x-shockwave-falsh");
        }

        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/swf/05122012_magicParticles.swf")]
        public ActionResult GetMagicParticles()
        {
            return SendFromLocal("swf/05122012_magicParticles.swf", "application/x-shockwave-falsh");
        }

        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/swf/05122012_dynamic.swf")]
        public ActionResult GetDynamic()
        {
            return SendFromLocal("swf/05122012_dynamic.swf", "application/x-shockwave-falsh");
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/track_game_status.php")]
        public ActionResult TrackGameStatus(string status, string installId, string user_id)
        {
            return Ok("");
        }

        [HttpGet("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/get_game_config.php")]
        public ActionResult GetGameConfig()
        {
            return SendFromLocal("config/game_config_zh.json", "application/json");
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/get_player_info.php")]
        public async Task<IActionResult> GetPlayerInfo(string userid, string user_key, string spdebug, string language)
        {
            var save = await _playerSaveService.GetPlayerSaveAsync(userid);
            if (save == null)
            {
                userid = HttpContext.User.Identity.Name;
                save = await _playerSaveService.CreatePlayerSaveAsync(userid, userid);
            }

            var root = new JsonObject();
            root.Add("map", JsonSerializer.SerializeToNode(
                save.DefaultMap,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            root.Add("playerInfo", JsonSerializer.SerializeToNode(
                save.PlayerInfo,
                new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower }));
            root.Add("privateState", JsonSerializer.SerializeToNode(
                save.PrivateState,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));

            return new JsonResult(root);
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/sync_error_track.php")]
        public ActionResult SyncErrorTrack(
            string userid, string user_key, string spdebug, string language,
            string error, string current_failed)
        {
            return Ok("");
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/command.php")]
        public async Task<ActionResult> Command([FromServices] CommandService commandService, [FromForm] string data)
        {
            var userId = HttpContext.User.Identity?.Name;
            if (userId == null)
            {
                return Redirect("/Login");
            }

            var commandDataString = data.Substring(65);
            var commandData = JsonSerializer.Deserialize<CommandData>(commandDataString, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ;
            await commandService.HandleCommandsAsync(userId, commandData.Commands);

            return Ok("""{"result": "success"}""");
        }

        public record CommandData(
            Command[] Commands, 
            int Ts, 
            string AccessToken, 
            int Tries, 
            string PublishActions)
        {
            [JsonPropertyName("first_number")]
            public int FirstNumber { get; set; }
        }

        private PhysicalFileResult SendFromLocal(string relativePath, string contentType)
        {
            Response.Headers.CacheControl = "no-store";
            return PhysicalFile(
                Directory.GetCurrentDirectory() + "/Assets/" + relativePath, 
                contentType);
        }
    }
}
