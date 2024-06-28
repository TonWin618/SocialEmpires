using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models.PlayerSaves;
using System.Text;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Players(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageIndex;
            
            var (pageCount, playerSaves) = await _playerSaveService.GetAllPlayerSavesAsync(pageIndex,pageSize);
            ViewData["PageCount"] = pageCount;

            var PlayerList = new List<Player>();
            foreach (var save in playerSaves) 
            {
                PlayerList.Add(new Player(save, GetUnitSummary(save)));
            }

            ViewData["PageData"] = PlayerList;

            return View();
        }
        public record Player(PlayerSave Save, string UnitSummary);

        [HttpGet]
        public async Task<IActionResult> EmpireMap(string playerId)
        {
            var player = await _playerSaveService.GetPlayerSaveAsync(playerId);
            if (player == null)
            {
                return NotFound();
            }

            var mapGridItems = new List<MapGridItem>();
            foreach (var item in player.DefaultMap.Items)
            {
                var info = _configFileService.GetItem(item.Id);
                mapGridItems.Add(new MapGridItem(item.X, item.Y, item.Id, info.ImgName, info.Width, info.Height));
            }

            ViewData["MapGridItems"] = mapGridItems;

            return View();
        }
        public record MapGridItem(int X, int Y, int Id, string Image, string Width, string Height);

        [HttpPost]
        public async Task ChangeCash(string playerId, [FromBody] int amount)
        {
            var save = await _playerSaveService.GetPlayerSaveAsync(playerId);
            if (save == null)
            {
                return;
            }
            save.PlayerInfo.Cash += amount;
        }

        private string GetUnitSummary(PlayerSave save)
        {
            var sb = new StringBuilder();
            var itemGroups = save.DefaultMap.Items.GroupBy(_ => _.Id);
            foreach (var group in itemGroups)
            {
                var id = group.Key;
                if ((id > 500 && id < 900) || (id > 2000 && id < 3000))
                {
                    var item = _configFileService.GetItem(id.ToString());
                    if (item == null)
                    {
                        continue;
                    }
                    sb.Append($"{item.Name}*{group.Count()}, ");
                }
            }
            return sb.ToString();
        }
    }
}
