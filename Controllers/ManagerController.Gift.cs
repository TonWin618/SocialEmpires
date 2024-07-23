using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialEmpires.Events;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Notifications;
using SocialEmpires.Models.PlayerSaves;
using SocialEmpires.Utils;

namespace SocialEmpires.Controllers
{
    public partial class ManagerController
    {
        [HttpGet]
        public IActionResult Gift(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            ViewData["PageData"] = _appDbContext
                .Notifications
                .WithLanguage()
                .Where(_ => _.Type == NotificationTypeNames.Gift)
                .Page(pageIndex, pageSize, out var pageCount)
                .ToList();
            ViewData["PageCount"] = pageCount;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "GiftManager")]
        public async Task<IActionResult> SendGift(string receiver, int cash, int gold, int stone, int wood, int food, int xp, string items)
        {
            var saves = new List<PlayerSave>();
            if (receiver == "ALL")
            {
                saves = await _playerSaveService.GetAllPlayerSavesAsync();
            }
            else
            {
                var save = await _playerSaveService.GetPlayerSaveAsync(receiver);
                if (save == null)
                {
                    ViewData["ErrorMessage"] = "UserNotFOUND";
                    return this.Redirect();
                }
                saves.Add(save);
            }

            foreach (var save in saves) 
            {
                save.PlayerInfo.Cash += cash;
                save.DefaultMap.Coins += gold;
                save.DefaultMap.Wood += wood;
                save.DefaultMap.Food += food;
                save.DefaultMap.Stone += stone;
                save.DefaultMap.Xp += xp;
                if (!items.IsNullOrEmpty())
                {
                    var itemIds = items.Split(',');
                    foreach (var itemIdString in itemIds)
                    {
                        var itemId = int.Parse(itemIdString);
                        var length = save.PrivateState.Gifts.Count;
                        if (length <= itemId)
                        {
                            for (var i = itemId - length + 1; i > 0; i--)
                            {
                                save.PrivateState.Gifts.Add(0);
                            }
                        }
                        save.PrivateState.Gifts[itemId]++;
                    }
                }
            }

            await _mediator.Publish(new SendGiftEvent()
            {
                PublisherId = UserId,
                UserIds = saves.Select(_ => _.Pid).ToList(),
                Cash = cash,
                Gold = gold,
                Wood = wood,
                Food = food,
                Stone = stone,
                Xp = xp,
                items = items == null ? [] : items.Split(',').Select(int.Parse).ToList()
            });

            return this.Redirect();
        }
    }
}
