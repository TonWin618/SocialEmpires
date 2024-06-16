using SocialEmpires.Models;
using SocialEmpires.Models.Enums;
using SocialEmpires.Services.Constants;

namespace SocialEmpires.Services
{
    public record Command(string Cmd, object[] Args);

    public class CommandService
    {
        private readonly ConfigFileService _configFileService;
        private readonly PlayerSaveService _playerSaveService;
        private readonly ILogger<CommandService> _logger;

        public CommandService(
            ConfigFileService configFileService,
            PlayerSaveService playerSaveService,
            ILogger<CommandService> logger) 
        {
            _configFileService = configFileService;
            _playerSaveService = playerSaveService;
            _logger = logger;
        }

        public async Task HandleCommandsAsync(string userId, IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                await HandleCommand(userId, command.Cmd, command.Args);
            }
        }

        public async Task HandleCommand(string userId, string cmd, params object[] args)
        {
            var save = await _playerSaveService.GetPlayerSaveAsync(userId);
            if(save == null)
            {
                return;
            }

            if (cmd == CommandNames.GAME_STATUS)
            {
                _logger.LogInformation(string.Join('|', args));
            }
            else if (cmd == CommandNames.BUY)
            {
                await HandleBuyCommandAsync(save, args);
            }
            else if (cmd == CommandNames.COMPLETE_TUTORIAL)
            {
                await HandleCompleteTutorialCommandAsync(save, args);
            }
            else if(cmd == CommandNames.MOVE)
            {
                await HandleMoveCommandAsync(save, args);
            }
            else if(cmd == CommandNames.COMPLETE_TUTORIAL)
            {
                await HandleCompleteTutorialCommandAsync(save, args);
            }
        }

        private void HandlePopUnitCommand(PlayerSave save, object[] args)
        {
            int buildingX = Convert.ToInt32(args[0]);
            int buildingY = Convert.ToInt32(args[1]);
            int townId = Convert.ToInt32(args[2]);
            int unitId = Convert.ToInt32(args[3]);
            bool placePoppedUnit = args.Length > 4;

            int unitX = 0;
            int unitY = 0;
            int unitFrame = 0; // Assuming unit_frame is an integer, adjust if it's another type

            if (placePoppedUnit)
            {
                unitX = Convert.ToInt32(args[4]);
                unitY = Convert.ToInt32(args[5]);
                unitFrame = Convert.ToInt32(args[6]);
            }

            _logger.LogInformation($"Pop {unitId} from ({buildingX},{buildingY}).");

            var map = save.Maps[townId];

            // Remove unit from building
            foreach (var item in map.Items)
            {
                if (item.X == buildingX && item.Y == buildingY)
                {
                    if (item.Units != null && item.Units.Count >= 7)
                    {
                        item.Units.RemoveAll(u => u == unitId);
                        break;
                    }
                }
            }

            if (placePoppedUnit)
            {
                // Spawn unit outside
                var collectedAtTimestamp = TimestampNow(); // Implement your timestamp logic
                var level = 0; // TODO: Adjust level logic as needed
                var orientation = 0; // TODO: Implement orientation logic if required

                map.Items.Add(new MapItem(unitId, unitX, unitY, orientation, collectedAtTimestamp, level));
            }
        }

        private async Task HandlePushUnitCommand(PlayerSave save, object[] args)
        {
            int unitX = Convert.ToInt32(args[0]);
            int unitY = Convert.ToInt32(args[1]);
            int unitId = Convert.ToInt32(args[2]);
            int buildingX = Convert.ToInt32(args[3]);
            int buildingY = Convert.ToInt32(args[4]);
            int townId = Convert.ToInt32(args[5]);

            _logger.LogInformation($"Push {unitId} to ({buildingX},{buildingY}).");

            var map = save.Maps[townId];

            // Unit into building
            foreach (var item in map.Items)
            {
                if (item.X == buildingX && item.Y == buildingY)
                {
                    if (item.Units == null)
                    {
                        item.Units = new List<int>();
                    }
                    item.Units.Add(unitId);
                    break;
                }
            }

            // Remove unit
            foreach (var item in map.Items.ToList())  // ToList() creates a copy to avoid modification errors
            {
                if (item.Id == unitId && item.X == unitX && item.Y == unitY)
                {
                    map.Items.Remove(item);
                    break;
                }
            }
        }

        private async Task HandleRewardMissionCommand(PlayerSave save, object[] args)
        {
            var townId = Convert.ToInt32(args[0]);
            var missionId = Convert.ToInt32(args[1]);

            _logger.LogInformation($"Reward mission {missionId}");

            var missions = await _configFileService.GetMission(missionId);
            save.Maps[townId].Coins += missions.Reward;

            save.PrivateState.RewardedMissions.Add(missionId);
        }

        private async Task HandleCompleteMissionCommand(PlayerSave save, object[] args)
        {
            int missionId = Convert.ToInt32(args[0]);
            bool skippedWithCash = Convert.ToBoolean(args[1]);

            _logger.LogInformation($"Complete mission {missionId}");

            if (skippedWithCash)
            {
                int cashToSubtract = 0; // TODO: Determine the value for cash subtraction
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - cashToSubtract, 0);
            }

            save.PrivateState.CompletedMissions.Add(missionId);
        }

        private async Task HandleKillCommand(PlayerSave save, object[] args)
        {
            int x = Convert.ToInt32(args[0]);
            int y = Convert.ToInt32(args[1]);
            int id = Convert.ToInt32(args[2]);
            int townId = Convert.ToInt32(args[3]);
            string type = Convert.ToString(args[4]);

            _logger.LogInformation($"Kill {id} from ({x},{y}).");

            var map = save.Maps[townId];
            var items = map.Items;

            // Find the item to kill and remove it
            var itemToRemove = items.FirstOrDefault(item => item.Id == id && item.X == x && item.Y == y);
            if (itemToRemove != null)
            {
                // Apply XP collection
                await ApplyCollectXpAsync(save, id);

                // Remove the item from the map
                items.Remove(itemToRemove);
            }
        }

        private async Task HandleSellCommand(PlayerSave save, object[] args)
        {
            int x = Convert.ToInt32(args[0]);
            int y = Convert.ToInt32(args[1]);
            int id = Convert.ToInt32(args[2]);
            int townId = Convert.ToInt32(args[3]);
            bool dontModifyResources = Convert.ToBoolean(args[4]);
            string reason = Convert.ToString(args[5]);

            _logger.LogInformation($"Remove {id} from ({x},{y}). Reason: {reason}");

            var map = save.Maps[townId];
            var items = map.Items;

            // Remove the item from the map
            var itemToRemove = items.FirstOrDefault(item => item.Id == id && item.X == x && item.Y == y);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }

            // Modify resources if needed
            if (!dontModifyResources)
            {
                double priceMultiplier = -0.05;
                var item = await _configFileService.GetItemAsync(id);
                string costType = item.CostType;
                
                if (costType != CostType.Cash)
                {
                    await ApplyCostAsync(save, id, priceMultiplier);
                }
            }

            // Handle reason 'KILL' (assuming you have a graveyard or similar logic)
            if (reason == "KILL")
            {
                // Implement logic to add to graveyard
                // Example: save.Graveyard.Add(itemToRemove);
            }
        }

        private async Task HandleCollectCommand(PlayerSave save, object[] args)
        {
            int x = Convert.ToInt32(args[0]);
            int y = Convert.ToInt32(args[1]);
            int townId = Convert.ToInt32(args[2]);
            int id = Convert.ToInt32(args[3]);
            int numUnitsContainedWhenHarvested = Convert.ToInt32(args[4]); // Assuming this affects multiplier logic elsewhere
            int resourceMultiplier = Convert.ToInt32(args[5]);
            int cashToSubtract = Convert.ToInt32(args[6]);

            _logger.LogInformation($"Collect {id}");

            var map = save.Maps[townId];
            await ApplyCollectAsync(save, id, resourceMultiplier);
            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - cashToSubtract, 0);
        }

        private async Task HandleCompleteTutorialCommandAsync(PlayerSave save, object[] args)
        {
            var tutorialStep = Convert.ToInt32(args[0]);
            if (tutorialStep > 31)
            {
                save.PlayerInfo.CompletedTutorial = save.PlayerInfo.CompletedTutorial = 1;
            }
        }

        private async Task HandleMoveCommandAsync(PlayerSave save, object[] args)
        {
            int ix = Convert.ToInt32(args[0]);
            int iy = Convert.ToInt32(args[1]);
            int id = Convert.ToInt32(args[2]);
            int newx = Convert.ToInt32(args[3]);
            int newy = Convert.ToInt32(args[4]);
            int frame = Convert.ToInt32(args[5]); // You may need to handle this parameter according to your requirements
            int townId = Convert.ToInt32(args[6]);
            string reason = Convert.ToString(args[7]); // "Unitat", "moveTo", "colisio", "MouseUsed"

            _logger.LogInformation($"Move {id} from ({ix},{iy}) to ({newx},{newy})");

            var map = save.Maps[townId]; // Assuming save.Maps is a dictionary with townId as key
            foreach (var item in map.Items)
            {
                if (item.Id == id && item.X == ix && item.Y == iy)
                {
                    item.X = newx;
                    item.Y = newy;
                    break;
                }
            }
        }

        private async Task HandleBuyCommandAsync(PlayerSave save, object[] args)
        {
            var id = Convert.ToInt32(args[0]);
            var x = Convert.ToInt32(args[1]);
            var y = Convert.ToInt32(args[2]);
            var frame = Convert.ToInt32(args[3]); // TODO ??
            var townId = Convert.ToInt32(args[4]);
            var dontModifyResources = Convert.ToBoolean(args[5]);
            var priceMultiplier = Convert.ToInt32(args[6]);
            var type = Convert.ToString(args[7]);

            _logger.LogInformation($"Add {id} at ({x},{y})");
            var collectedAtTimestamp = TimestampNow();
            var level = 0; // TODO 
            var orientation = 0;
            var map = save.DefaultMap; // Adjusted from save["maps"][townId] to save.DefaultMap

            if (!dontModifyResources)
            {
                await ApplyCollectAsync(save, id, priceMultiplier);
                await ApplyCollectXpAsync(save, id);
            }
            map.Items.Add(new MapItem(id, x, y, orientation, collectedAtTimestamp, level));
        }

        private async Task ApplyCollectAsync(PlayerSave save, int id, double multiplier)
        {
            var item = await _configFileService.GetItemAsync(id);
            if (item == null)
            {
                return;
            }
            var collect = int.Parse(item.Collect) * multiplier;
            switch (item.CollectType)
            {
                case CostType.Wood:
                    save.DefaultMap.Wood += (int)collect;
                    break;
                case CostType.Gold:
                    save.DefaultMap.Coins += (int)collect;
                    break;
                case CostType.Cash:
                    save.PlayerInfo.Cash += (int)collect;
                    break;
                case CostType.Stone:
                    save.DefaultMap.Stone += (int)collect;
                    break;
                case CostType.Food:
                    save.DefaultMap.Food += (int)collect;
                    break;
            }

            var collectXp = int.Parse(item.CollectXp);
            save.DefaultMap.Xp += collectXp;
        }

        private async Task ApplyCostAsync(PlayerSave save, int id, double multiplier)
        {
            var item = await _configFileService.GetItemAsync(id);
            if (item == null)
            {
                return;
            }
            var cost = multiplier * int.Parse(item.Cost);

            switch (item.CostType)
            {
                case CostType.Wood:
                    save.DefaultMap.Wood = Math.Max(save.DefaultMap.Wood - (int)cost, 0);
                    break;
                case CostType.Gold:
                    save.DefaultMap.Coins = Math.Max(save.DefaultMap.Coins - (int)cost, 0);
                    break;
                case CostType.Cash:
                    save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - (int)cost, 0);
                    break;
                case CostType.Stone:
                    save.DefaultMap.Stone = Math.Max(save.DefaultMap.Stone - (int)cost, 0);
                    break;
                case CostType.Food:
                    save.DefaultMap.Food = Math.Max(save.DefaultMap.Food - (int)cost, 0);
                    break;
            }
        }

        private async Task ApplyCollectXpAsync(PlayerSave save, int id)
        {
            var item = await _configFileService.GetItemAsync(id);
            if (item == null)
            {
                return;
            }
            var collectXp = int.Parse(item.CollectXp);
            save.DefaultMap.Xp += collectXp;
        }
        
        private long TimestampNow()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
