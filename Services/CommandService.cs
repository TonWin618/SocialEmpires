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
                HandleBuyCommandAsync(save, args);
            }
            else if (cmd == CommandNames.COMPLETE_TUTORIAL)
            {
                HandleCompleteTutorialCommandAsync(save, args);
            }
            else if (cmd == CommandNames.MOVE)
            {
                HandleMoveCommandAsync(save, args);
            }
            else if (cmd == CommandNames.COLLECT)
            {
                HandleCollectCommand(save, args);
            }
            else if (cmd == CommandNames.STORE_ITEM)
            {
                HandleStoreItemCommand(save, args);
            }
            else if (cmd == CommandNames.EXCHANGE_CASH)
            {
                HandleExchangeCashCommand(save, args);
            }
            else if (cmd == CommandNames.NAME_MAP)
            {
                HandleNameMapCommand(save, args);
            }
            else if (cmd == CommandNames.EXPAND)
            {
                HandleExpandCommand(save, args);
            }
            else if (cmd == CommandNames.RT_PUBLISH_SCORE)
            {
                HandleRTPublishScoreCommand(save, args);
            }
            else if (cmd == CommandNames.RT_LEVEL_UP)
            {
                HandleRTLevelUpCommand(save, args);
            }
            else if (cmd == CommandNames.POP_UNIT)
            {
                HandlePopUnitCommand(save, args);
            }
            else if (cmd == CommandNames.PUSH_UNIT)
            {
                HandlePushUnitCommand(save, args);
            }
            else if (cmd == CommandNames.REWARD_MISSION)
            {
                HandleRewardMissionCommand(save, args);
            }
            else if (cmd == CommandNames.COMPLETE_MISSION)
            {
                HandleCompleteMissionCommand(save, args);
            }
            else if (cmd == CommandNames.KILL)
            {
                HandleKillCommand(save, args);
            }
            else if (cmd == CommandNames.SELL)
            {
                HandleSellCommand(save, args);
            }
            else if(cmd == CommandNames.PLACE_GIFT)
            {
                HandlePlaceGiftCommand(save, args);
            }
            else if(cmd == CommandNames.SELL_GIFT)
            {
                HandleSellGiftCommand(save, args);
            }
            else
            {
                _logger.LogWarning($"Unknown command: {cmd}");
            }
        }

        private void HandlePlaceGiftCommand(PlayerSave save, object[] args)
        {
            int itemId = Convert.ToInt32(args[0]);
            int x = Convert.ToInt32(args[1]);
            int y = Convert.ToInt32(args[2]);
            int townId = Convert.ToInt32(args[3]); // Assuming this is correct based on your logic
                                                   // args[4] is unknown and not used in the implementation
            _logger.LogInformation($"Add {itemId} at ({x},{y})");

            var items = save.Maps[townId].Items;
            int orientation = 0; // TODO: Determine the orientation logic
            var collectedAtTimestamp = TimestampNow(); // Assuming a function for current timestamp
            int level = 0;

            // Add the gift item to the map's items
            items.Add(new MapItem(itemId, x, y, orientation, collectedAtTimestamp, level));

            // Decrease the count of the gift in private state
            save.PrivateState.Gifts[itemId]--;

            // Remove excess zeros from the end of the gifts list if necessary
            while (save.PrivateState.Gifts.Count > 0 && save.PrivateState.Gifts[^1] == 0)
            {
                save.PrivateState.Gifts.RemoveAt(save.PrivateState.Gifts.Count - 1);
            }
        }

        private void HandleSellGiftCommand(PlayerSave save, object[] args)
        {
            int itemId = Convert.ToInt32(args[0]);
            int townId = Convert.ToInt32(args[1]);
            _logger.LogInformation($"Gift {itemId} sold on town: {townId}");

            var gifts = save.PrivateState.Gifts;
            gifts[itemId]--;

            // Remove excess zeros from the end of the gifts list if necessary
            while (gifts.Count > 0 && gifts[^1] == 0)
            {
                gifts.RemoveAt(gifts.Count - 1);
            }

            // Apply cost if applicable (assuming apply_cost_async is used elsewhere)
            double priceMultiplier = -0.05;
            if (_configFileService.GetItem(itemId).CostType != CostType.Cash)
            {
                ApplyCostAsync(save, itemId, priceMultiplier);
            }
        }

        private void HandleStoreItemCommand(PlayerSave save, object[] args)
        {
            int x = Convert.ToInt32(args[0]);
            int y = Convert.ToInt32(args[1]);
            int townId = Convert.ToInt32(args[2]);
            int itemId = Convert.ToInt32(args[3]);
            _logger.LogInformation($"Store {itemId} from ({x},{y})");

            var map = save.Maps[townId];
            var items = map.Items;

            // Remove item from map's items
            foreach (var item in items)
            {
                if (item.Id == itemId && item.X == x && item.Y == y)
                {
                    items.Remove(item);
                    break;
                }
            }

            // Ensure gifts list is sufficient to access the item_id
            int length = save.PrivateState.Gifts.Count;
            if (length <= itemId)
            {
                for (int i = itemId - length + 1; i > 0; i--)
                {
                    save.PrivateState.Gifts.Add(0);
                }
            }

            // Increment the count of the item_id in gifts
            save.PrivateState.Gifts[itemId]++;
        }

        private void HandleExchangeCashCommand(PlayerSave save, object[] args)
        {
            int townId = Convert.ToInt32(args[0]);
            _logger.LogInformation("Exchange cash -> coins.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - 5, 0); // Assuming a function for editing resources is used elsewhere
            save.Maps[townId].Coins += 2500;
        }

        private void HandleNameMapCommand(PlayerSave save, object[] args)
        {
            int townId = Convert.ToInt32(args[0]);
            string newName = Convert.ToString(args[1]);
            _logger.LogInformation($"Map name changed to '{newName}'.");

            save.PlayerInfo.MapNames[townId] = newName;
        }

        private void HandleExpandCommand(PlayerSave save, object[] args)
        {
            int landId = Convert.ToInt32(args[0]);
            string resource = Convert.ToString(args[1]);
            int townId = Convert.ToInt32(args[2]);

            _logger.LogInformation($"Expansion {landId} purchased");

            var map = save.DefaultMap;
            var expansions = map.Expansions;

            if (expansions.Contains(landId))
            {
                return;
            }

            // Subtract resources
            var expansionPrices = _configFileService.ExpansionPrices;
            var exp = expansionPrices[expansions.Count - 1];

            if (resource == "gold")
            {
                int toSubtract = exp.Coins;
                map.Coins = Math.Max(map.Coins - toSubtract, 0);
            }
            else if (resource == "cash")
            {
                int toSubtract = exp.Cash;
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - toSubtract, 0);
            }

            // Add expansion
            expansions.Add(landId);
        }

        private void HandleRTPublishScoreCommand(PlayerSave save, object[] args)
        {
            int newXp = Convert.ToInt32(args[0]);
            _logger.LogInformation($"xp set to {newXp}");

            var map = save.Maps[0]; // Assuming xp is general across maps, adjust if necessary
            map.Xp = newXp;
            var levels = _configFileService.Levels;
            var level = levels.FirstOrDefault(_ => _.ExpRequired > newXp);
            map.Level = levels.IndexOf(level);//GetLevelFromXp(newXp);
        }

        private void HandleRTLevelUpCommand(PlayerSave save, object[] args)
        {
            int newLevel = Convert.ToInt32(args[0]);
            _logger.LogInformation($"Level Up!: {newLevel}");

            var map = save.Maps[0]; // Assuming xp is general across maps, adjust if necessary
            map.Level = newLevel;

            int currentXp = map.Xp;
            var level = _configFileService.Levels[Math.Max(0, newLevel-1)];
            int minExpectedXp = level.ExpRequired;
            map.Xp = Math.Max(minExpectedXp, currentXp);
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

        private void HandlePushUnitCommand(PlayerSave save, object[] args)
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

        private void HandleRewardMissionCommand(PlayerSave save, object[] args)
        {
            var townId = Convert.ToInt32(args[0]);
            var missionId = Convert.ToInt32(args[1]);

            _logger.LogInformation($"Reward mission {missionId}");

            var missions = _configFileService.Missions
                .FirstOrDefault(_ => _.Id == missionId);

            save.Maps[townId].Coins += missions.Reward;
            save.PrivateState.RewardedMissions.Add(missionId);
        }

        private void HandleCompleteMissionCommand(PlayerSave save, object[] args)
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

        private void HandleKillCommand(PlayerSave save, object[] args)
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
                ApplyCollectXpAsync(save, id);

                // Remove the item from the map
                items.Remove(itemToRemove);
            }
        }

        private void HandleSellCommand(PlayerSave save, object[] args)
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
                var item = _configFileService.GetItem(id);
                string costType = item.CostType;
                
                if (costType != CostType.Cash)
                {
                    ApplyCostAsync(save, id, priceMultiplier);
                }
            }

            // Handle reason 'KILL' (assuming you have a graveyard or similar logic)
            if (reason == "KILL")
            {
                // Implement logic to add to graveyard
                // Example: save.Graveyard.Add(itemToRemove);
            }
        }

        private void HandleCollectCommand(PlayerSave save, object[] args)
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
            ApplyCollectAsync(save, id, resourceMultiplier);
            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - cashToSubtract, 0);
        }

        private void HandleCompleteTutorialCommandAsync(PlayerSave save, object[] args)
        {
            var tutorialStep = Convert.ToInt32(args[0]);
            if (tutorialStep > 31)
            {
                save.PlayerInfo.CompletedTutorial = save.PlayerInfo.CompletedTutorial = 1;
            }
        }

        private void HandleMoveCommandAsync(PlayerSave save, object[] args)
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

        private void HandleBuyCommandAsync(PlayerSave save, object[] args)
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
                ApplyCollectAsync(save, id, priceMultiplier);
                ApplyCollectXpAsync(save, id);
            }
            map.Items.Add(new MapItem(id, x, y, orientation, collectedAtTimestamp, level));
        }

        private void ApplyCollectAsync(PlayerSave save, int id, double multiplier)
        {
            var item = _configFileService.GetItem(id);
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

        private void ApplyCostAsync(PlayerSave save, int id, double multiplier)
        {
            var item = _configFileService.GetItem(id);
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

        private void ApplyCollectXpAsync(PlayerSave save, int id)
        {
            var item = _configFileService.GetItem(id);
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
