using Microsoft.EntityFrameworkCore;
using SocialEmpires.Models;
using SocialEmpires.Utils;

namespace SocialEmpires.Services
{
    public class PlayerSaveService
    {
        private readonly AppDbContext _appDbContext;

        public PlayerSaveService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PlayerSave> CreatePlayerSaveAsync(string userId, string name)
        {
            var playerSave = PlayerSave.Create(userId, name);
            await _appDbContext.PlayerSaves.AddAsync(playerSave);
            return playerSave;
        }

        public async Task<(int pageCount, List<PlayerSave>)> GetAllPlayerSavesAsync(int pageIndex, int pageSize)
        {
            var saves = await _appDbContext.PlayerSaves
                .Page(pageIndex, pageSize,out int pageCount)
                .ToListAsync();
            return (pageCount, saves);
        }

        public async Task<PlayerSave?> GetPlayerSaveAsync(string playerId)
        {
            return await _appDbContext.PlayerSaves
                .Include(_ => _.PlayerInfo)
                .Include(_ => _.Maps)
                .Include(_ => _.PrivateState)
                .FirstOrDefaultAsync(_ => _.Pid == playerId);
        }

        public async Task DeletePlayerSaveAsync(string playerId)
        {
            var save = await _appDbContext.PlayerSaves.FirstOrDefaultAsync(_ => _.Pid == playerId);
            if (save != null)
            {
                _appDbContext.Remove(save);
            }
        }
    }
}
