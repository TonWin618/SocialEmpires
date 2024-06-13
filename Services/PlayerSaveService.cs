using SocialEmpires.Models;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public class PlayerSaveService
    {
        private readonly AppDbContext _appDbContext;

        public PlayerSaveService(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        private List<PlayerSave> LoadAllPlayerSaves()
        {
            return new List<PlayerSave>();
        }

        public async Task<PlayerSave> CreatePlayerSaveAsync(string userId, string name)
        {
            var playerSave = PlayerSave.Create(userId, name);
            return playerSave;
        }

        public async Task<PlayerSave?> GetPlayerSaveAsync(string playerId)
        {
            return _appDbContext.PlayerSaves.FirstOrDefault(_ => _.Pid == playerId);
        }

        public async Task DeletePlayerSaveAsync(string playerId)
        {
            throw new NotImplementedException();
        }
    }
}
