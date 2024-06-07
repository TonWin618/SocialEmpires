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

        public async Task CreatePlayerSave(PlayerSave save)
        {
            playerSaves.Add(save);
        }

        public async Task<PlayerSave?> GetPlayerSave(long playerId)
        {
            return playerSaves.FirstOrDefault(_ => _.PlayerInfo.Pid == playerId);
        }

        public async Task UpdatePlayerSave(long playerId)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePlayerSave(long playerId)
        {
            throw new NotImplementedException();
        }
    }
}
