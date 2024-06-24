using SocialEmpires.Models;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleStartQuestCommand(PlayerSave save, JsonElement[] args)
        {
            var questId = args[0].GetInt32();
            var townId = args[1].GetInt32();
            _logger.LogInformation($"Start quest {questId}");
            // Additional logic for starting the quest can be added here if needed
        }

        private void HandleEndQuestCommand(PlayerSave save, JsonElement[] args)
        {
            var data = JsonDocument.Parse(args[0].GetString()).RootElement;

            var townId = data.GetProperty("map").GetInt32();
            var goldGained = data.GetProperty("resources").GetProperty("g").GetInt32();
            var xpGained = data.GetProperty("resources").GetProperty("x").GetInt32();
            var units = data.GetProperty("units").EnumerateArray().ToArray();
            var win = data.GetProperty("win").GetInt32() == 1;
            var durationSec = data.GetProperty("duration").GetInt32();
            var voluntaryEnd = data.GetProperty("voluntary_end").GetInt32() == 1;
            var questId = data.GetProperty("quest_id").GetInt32();
            var itemRewards = data.TryGetProperty("item_rewards", out var itemRewardsProperty) ?
                itemRewardsProperty.EnumerateArray().ToArray() : null;
            var activatorsLeft = data.TryGetProperty("activators_left", out var activatorsLeftProperty) ?
                activatorsLeftProperty.EnumerateArray().ToArray() : null;
            var difficulty = data.GetProperty("difficulty");

            var map = save.Maps[townId];
            map.Coins += goldGained;
            map.Xp += xpGained;

            var privateState = save.PrivateState;
            privateState.UnlockedQuestIndex = Math.Max(questId + 1, privateState.UnlockedQuestIndex);

            // Uncomment and add logic if needed
            // privateState.QuestsRank = TODO;
            // map.QuestTimes[questId] = TODO;
            // map.LastQuestTimes[questId] = TODO;

            _logger.LogInformation($"Ended quest {questId}.");
        }

        private void HandleRewardMissionCommand(PlayerSave save, JsonElement[] args)
        {
            var townId = args[0].GetInt32();
            var missionId = args[1].GetInt32();

            _logger.LogInformation($"Reward mission {missionId}");

            var missions = _configFileService.Missions
                .FirstOrDefault(_ => _.Id == missionId);

            save.Maps[townId].Coins += missions?.Reward ?? 0;
            save.PrivateState.RewardedMissions.Add(missionId);
        }

        private void HandleCompleteMissionCommand(PlayerSave save, JsonElement[] args)
        {
            var missionId = args[0].GetInt32();
            var skippedWithCash = args[1].GetInt32();

            _logger.LogInformation($"Complete mission {missionId}");

            if (skippedWithCash == 1)
            {
                var cashToSubtract = 0; // TODO: Determine the value for cash subtraction
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - cashToSubtract, 0);
            }

            save.PrivateState.CompletedMissions.Add(missionId);
        }



        private void HandleCompleteTutorialCommandAsync(PlayerSave save, JsonElement[] args)
        {
            var tutorialStep = args[0].GetInt32();
            if (tutorialStep >= 31)
            {
                save.PlayerInfo.CompletedTutorial = 1;
            }
        }
    }
}
