using SocialEmpires.Models;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public class PlayerSaveService
    {
        private readonly List<PlayerSave> playerSaves;

        public PlayerSaveService() { }

        private async Task LoadAllPlayerSaves()
        {

        }

        public async Task CreatePlayerSaveAsync(PlayerSave save)
        {
            playerSaves.Add(save);
        }

        public async Task<PlayerSave?> GetPlayerSaveAsync(long playerId)
        {
            return playerSaves.FirstOrDefault(_ => _.PlayerInfo.Pid == playerId);
        }

        public async Task UpdatePlayerSaveAsync(long playerId)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePlayerSaveAsync(long playerId)
        {
            throw new NotImplementedException();
        }
    }
}
