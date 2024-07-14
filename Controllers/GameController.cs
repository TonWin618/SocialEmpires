using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialEmpires.Dtos;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Options;
using SocialEmpires.Services;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "User")]
    public class GameController : Controller
    {
        private readonly FileDirectoriesOptions _options;
        private readonly ConfigService _configService;
        private readonly IMapper _mapper;

        private JsonSerializerOptions camelCaseJsonoptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private JsonSerializerOptions snakeCaseoptions = new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
        public GameController(IOptions<FileDirectoriesOptions> options, ConfigService configService, IMapper mapper)
        {
            _options = options.Value;
            _configService = configService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.IsAdmin = HttpContext.User.IsInRole("Admin");
            ViewData["UserId"] = HttpContext!.User!.Identity!.Name!;
            ViewData["DateTime"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return View();
        }

        [HttpGet("CreatePlayerSaveForTest")]
        public async Task<ActionResult> CreatePlayerSaveForTest([FromServices] PlayerSaveService _playerSaveService)
        {
            var id = Guid.NewGuid().ToString();
            var user = await _playerSaveService.CreatePlayerSaveAsync(id, id);

            return Ok(id);
        }

        [HttpGet("/avatar/{userid}.png")]
        public ActionResult GetAvatar()
        {
            return SendFromLocal(Path.Combine(_options.Uploads, "Avatars/example.png"));
        }

        [HttpGet("crossdomain.xml")]
        public ActionResult GetCrossdomainXml()
        {
            return SendFromLocal(Path.Combine(_options.Assets, "crossdomain.xml"));
        }

        [HttpGet("/default01.static.socialpointgames.com/static/socialempires/{*path}")]
        public ActionResult GetStaticAssets(string path)
        {
            return SendFromLocal(Path.Combine(_options.Assets, path));
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/track_game_status.php")]
        public ActionResult TrackGameStatus(string status, string installId, string user_id)
        {
            return Ok("");
        }

        [HttpGet("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/get_game_config.php")]
        public ActionResult GetGameConfig()
        {
            var lowerSnakeCaseoptions = new JsonSerializerOptions() 
            { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower }
            .WithLanguage("en");

            var upperSnakeCaseoptions = new JsonSerializerOptions()
            { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper }
            .WithLanguage("en");

            var root = new JsonObject();

            var localizationStringDtos = _mapper.Map<List<LocalizationStringDto>>(_configService.LocalizationStrings);
            root.Add("localization_strings", JsonSerializer.SerializeToNode(localizationStringDtos, lowerSnakeCaseoptions));

            root.Add("categories", JsonSerializer.SerializeToNode(_configService.Categories, lowerSnakeCaseoptions));

            var itemDtos = _mapper.Map<List<ItemDto>>(_configService.Items);
            root.Add("items", JsonSerializer.SerializeToNode(itemDtos, lowerSnakeCaseoptions));

            var expansionPricesDtos = _mapper.Map<List<ExpansionPriceDto>>(_configService.ExpansionPrices);
            root.Add("expansion_prices", JsonSerializer.SerializeToNode(expansionPricesDtos, lowerSnakeCaseoptions));

            var levelsDtos = _mapper.Map<List<LevelDto>>(_configService.Levels);
            root.Add("levels", JsonSerializer.SerializeToNode(levelsDtos, lowerSnakeCaseoptions));

            var honorLevelsDtos = _mapper.Map<List<HonorLevelDto>>(_configService.HonorLevels);
            root.Add("honor_levels", JsonSerializer.SerializeToNode(honorLevelsDtos, lowerSnakeCaseoptions));

            var neighborAssistsDtos = _mapper.Map<List<NeighborAssistDto>>(_configService.NeighborAssists);
            root.Add("neighbor_assists", JsonSerializer.SerializeToNode(neighborAssistsDtos, lowerSnakeCaseoptions));

            var townPricesDtos = _mapper.Map<List<TownPriceDto>>(_configService.TownPrices);
            root.Add("town_prices", JsonSerializer.SerializeToNode(townPricesDtos, lowerSnakeCaseoptions));

            var mapPricesDtos = _mapper.Map<List<MapPriceDto>>(_configService.MapPrices);
            root.Add("map_prices", JsonSerializer.SerializeToNode(mapPricesDtos, lowerSnakeCaseoptions));

            var findableItemsDtos = _mapper.Map<List<FindableItemDto>>(_configService.FindableItems);
            root.Add("findable_items", JsonSerializer.SerializeToNode(findableItemsDtos, lowerSnakeCaseoptions));

            var missionsDtos = _mapper.Map<List<MissionDto>>(_configService.Missions);
            root.Add("missions", JsonSerializer.SerializeToNode(missionsDtos, lowerSnakeCaseoptions));

            var offerPacksDtos = _mapper.Map<List<OfferPackDto>>(_configService.OfferPacks);
            root.Add("offer_packs", JsonSerializer.SerializeToNode(offerPacksDtos, lowerSnakeCaseoptions));

            root.Add("images", JsonSerializer.SerializeToNode(_configService.Images, lowerSnakeCaseoptions));

            var socialItemsDtos = _mapper.Map<List<SocialItemDto>>(_configService.SocialItems);
            root.Add("social_items", JsonSerializer.SerializeToNode(socialItemsDtos, lowerSnakeCaseoptions));

            root.Add("globals", JsonSerializer.SerializeToNode(_configService.Globals, upperSnakeCaseoptions));

            var magicsDtos = _mapper.Map<List<MagicDto>>(_configService.Magics);
            root.Add("magics", JsonSerializer.SerializeToNode(magicsDtos, lowerSnakeCaseoptions));

            var levelRankingRewardDtos = _mapper.Map<List<LevelRankingRewardDto>>(_configService.LevelRankingReward);
            root.Add("level_ranking_reward", JsonSerializer.SerializeToNode(levelRankingRewardDtos, lowerSnakeCaseoptions));

            root.Add("tournament_type", JsonSerializer.SerializeToNode(_configService.TournamentType, lowerSnakeCaseoptions));

            var dartsItemsDtos = _mapper.Map<List<DartsItemDto>>(_configService.DartsItems);
            root.Add("darts_items", JsonSerializer.SerializeToNode(dartsItemsDtos, lowerSnakeCaseoptions));

            return new JsonResult(root);
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/get_player_info.php")]
        public async Task<IActionResult> GetPlayerInfo(string userid, string user_key, string spdebug, string language,
            [FromForm]string? user,
            [FromServices] PlayerSaveService _playerSaveService)
        {
            if (user != null && user.StartsWith("100000"))
            {
                return SendFromLocal(Path.Combine(_options.Maps, $"{user}.json"));
            }

            var save = await _playerSaveService.GetPlayerSaveAsync(userid);
            if (save == null)
            {
                userid = HttpContext!.User!.Identity!.Name!;
                save = await _playerSaveService.CreatePlayerSaveAsync(userid, userid);
            }

            var root = new JsonObject();
            

            root.Add("map", JsonSerializer.SerializeToNode(
                save.DefaultMap,
                camelCaseJsonoptions));
            root.Add("playerInfo", JsonSerializer.SerializeToNode(
                save.PlayerInfo,
                snakeCaseoptions));
            root.Add("privateState", JsonSerializer.SerializeToNode(
                save.PrivateState,
                camelCaseJsonoptions));
            root.Add("processed_errors", 0);
            root.Add("result", "ok");
            root.Add("timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            return new JsonResult(root);
        }

        [HttpPost("/dynamic.flash1.dev.socialpoint.es/appsfb/socialempiresdev/srvempires/sync_error_track.php")]
        public ActionResult SyncErrorTrack(
            string userid, string user_key, string spdebug, string language,
            string error, string current_failed)
        {
            return Redirect("/");
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
            var commandData = JsonSerializer.Deserialize<CommandData>(commandDataString, camelCaseJsonoptions);

            if(commandData == null)
            {
                return Ok("""{"result": "failed"}""");
            }

            await commandService.HandleCommandsAsync(userId, commandData.Commands);

            return Ok("""{"result": "success"}""");
        }

        public record CommandData(Command[] Commands, int Ts, string AccessToken, int Tries, string PublishActions)
        {
            [JsonPropertyName("first_number")]
            public int FirstNumber { get; set; }
        }

        private ActionResult SendFromLocal(string relativePath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            if (!Path.Exists(path))
            {
                return NotFound();
            }

            var contentType = Path.GetExtension(relativePath) switch
            {
                "swf" => "application/x-shockwave-falsh",
                "xml" => "application/xml",
                "json" => "application/json",
                "png" => "image/png",
                "jpg" => "image/jpeg",
                _ => "application/octet-stream"
            };
            
            return PhysicalFile(path, contentType);
        }
    }
}
