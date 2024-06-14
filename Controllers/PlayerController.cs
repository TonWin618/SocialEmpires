using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    [ApiController]
    [Route("api/admin/players")]
    public class PlayerController:ControllerBase
    {
        private readonly PlayerSaveService _playerSaveService;
        public PlayerController(PlayerSaveService playerSaveService) 
        {
            _playerSaveService = playerSaveService;
        }

        [HttpPost("{playerId}/changeCash")]
        public async Task ChangeCash(string playerId, [FromBody] int amount)
        {
            var save = await _playerSaveService.GetPlayerSaveAsync(playerId);
            if (save == null)
            {
                return;
            }
            save.PlayerInfo.Cash += amount;
        }
    }
}
