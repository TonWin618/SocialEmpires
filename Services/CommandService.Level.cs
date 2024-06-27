using SocialEmpires.Models;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleRTPublishScoreCommand(PlayerSave save, JsonElement[] args)
        {
            var newXp = args[0].GetInt32();
            _logger.LogInformation("xp set to {newXp}", newXp);

            var map = save.Maps[0]; //TODO: Assuming xp is general across maps, adjust if necessary
            map.Xp = newXp;
            var levels = _configFileService.Levels;
            var level = levels.First(_ => _.ExpRequired > newXp);
            map.Level = levels.IndexOf(level);
        }

        private void HandleRTLevelUpCommand(PlayerSave save, JsonElement[] args)
        {
            var newLevel = args[0].GetInt32();
            _logger.LogInformation("Level Up!: {newLevel}", newLevel);

            var map = save.Maps[0]; //TODO: Assuming xp is general across maps, adjust if necessary
            map.Level = newLevel;

            var currentXp = map.Xp;
            var level = _configFileService.Levels[Math.Max(0, newLevel - 1)];
            var minExpectedXp = level.ExpRequired;
            map.Xp = Math.Max(minExpectedXp, currentXp);
        }
    }
}
