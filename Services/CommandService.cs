using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models.Enums;
using SocialEmpires.Services.Constants;
using SQLitePCL;
using System.Reflection.Metadata;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public record Command(string Cmd, JsonElement[] Args);
    
    public class CommandService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ConfigFileService _configFileService;
        private readonly PlayerSaveService _playerSaveService;
        private readonly ILogger<CommandService> _logger;

        public CommandService(
            AppDbContext appDbContext,
            ConfigFileService configFileService,
            PlayerSaveService playerSaveService,
            ILogger<CommandService> logger) 
        {
            _appDbContext = appDbContext;
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

        public async Task HandleCommand(string userId, string cmd, params JsonElement[] args)
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

        private void HandleAddCollectableCommand(PlayerSave save, object[] args)
        {
            int collectionId = Convert.ToInt32(args[0]);
            int collectibleId = Convert.ToInt32(args[1]);
            var collections = save.PrivateState.Collections;
        }

        private void HandleStartQuestCommand(PlayerSave save, object[] args)
        {
            int questId = Convert.ToInt32(args[0]);
            int townId = Convert.ToInt32(args[1]);
            _logger.LogInformation($"Start quest {questId}");
            // Additional logic for starting the quest can be added here if needed
        }

        private void HandleEndQuestCommand(PlayerSave save, object[] args)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(args[0]));
            int townId = Convert.ToInt32(data["map"]);
            int goldGained = Convert.ToInt32(data["resources"]["g"]);
            int xpGained = Convert.ToInt32(data["resources"]["x"]);
            var units = data["units"] as JArray;
            bool win = Convert.ToInt32(data["win"]) == 1;
            int durationSec = Convert.ToInt32(data["duration"]);
            bool voluntaryEnd = Convert.ToInt32(data["voluntary_end"]) == 1;
            int questId = Convert.ToInt32(data["quest_id"]);
            var itemRewards = data.ContainsKey("item_rewards") ? data["item_rewards"] as JArray : null;
            var activatorsLeft = data.ContainsKey("activators_left") ? data["activators_left"] as JArray : null;
            var difficulty = data["difficulty"];

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

        private void HandleSetStrategyCommand(PlayerSave save, object[] args)
        {
            int strategyType = Convert.ToInt32(args[0]);
            string typeName = GetStrategyType(strategyType);
            save.PrivateState.Strategy = strategyType;
            _logger.LogInformation($"Set defense strategy type to {typeName}");
        }


        private void HandleBuySuperOfferPackCommand(PlayerSave save, object[] args)
        {
            int townId = Convert.ToInt32(args[0]);
            int superOfferPackId = Convert.ToInt32(args[1]); // assuming this is the super offer pack ID
            string items = Convert.ToString(args[2]);
            int cashUsed = Convert.ToInt32(args[3]);

            var map = save.Maps[townId];
            var itemArray = items.Split(',');

            foreach (var item in itemArray)
            {
                int itemId = Convert.ToInt32(item);
                var gifts = save.PrivateState.Gifts;
                if (gifts.Count <= itemId)
                {
                    for (int i = itemId - gifts.Count + 1; i > 0; i--)
                    {
                        gifts.Add(0);
                    }
                }
                gifts[itemId] += 1;
            }

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - cashUsed, 0);
            _logger.LogInformation($"Used {cashUsed} cash to buy super offer pack!");
        }

        private void HandleNextMonsterCommand(PlayerSave save)
        {
            _logger.LogInformation("Monster Step reset and Monster Number increased.");

            var privateState = save.PrivateState;
            privateState.StepMonsterNumber = 0;
            privateState.MonsterNumber += 1;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer
        }

        private void HandleNextMonsterStepCommand(PlayerSave save)
        {
            _logger.LogInformation("Monster Step increased.");

            var privateState = save.PrivateState;
            privateState.StepMonsterNumber += 1;
            privateState.TimeStampTakeCareMonster = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }

        private void HandleDesactivateMonsterCommand(PlayerSave save)
        {
            _logger.LogInformation("Monster nest deactivated.");

            var privateState = save.PrivateState;
            privateState.MonsterNestActive = 0;
            privateState.StepMonsterNumber = 0;
            privateState.MonsterNumber = 0;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer if any
        }

        private void HandleActivateMonsterCommand(PlayerSave save, object[] args)
        {
            string currency = Convert.ToString(args[0]);
            _logger.LogInformation("Monster nest activated.");

            if (currency == "c")
            {
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - 50, 0);
            }
            else if (currency == "g")
            {
                var map = save.Maps[0];
                map.Coins = Math.Max(map.Coins - 100000, 0);
            }

            var privateState = save.PrivateState;
            privateState.MonsterNestActive = 1;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer if any
        }

        private void HandleMonsterBuyStepCashCommand(PlayerSave save, object[] args)
        {
            int price = Convert.ToInt32(args[0]);
            _logger.LogInformation("Buy monster step with cash.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - price, 0);
            save.PrivateState.TimeStampTakeCareMonster = -1; // Remove timer
        }


        private void HandleOrientCommand(PlayerSave save, object[] args)
        {
            int x = Convert.ToInt32(args[0]);
            int y = Convert.ToInt32(args[1]);
            int newOrientation = Convert.ToInt32(args[2]);
            int townId = Convert.ToInt32(args[3]);

            _logger.LogInformation($"Item at ({x},{y}) changed to orientation {newOrientation}");

            var map = save.Maps[townId];
            foreach (var item in map.Items)
            {
                if (item.X == x && item.Y == y)
                {
                    item.Orientation = newOrientation;
                    break;
                }
            }
        }

        private void HandleResurrectHeroCommand(PlayerSave save, object[] args)
        {
            int unitId = Convert.ToInt32(args[0]);
            int x = Convert.ToInt32(args[1]);
            int y = Convert.ToInt32(args[2]);
            int townId = Convert.ToInt32(args[3]);
            bool usedPotion = args.Length > 4 && Convert.ToString(args[4]) == "1";

            _logger.LogInformation($"Resurrect {unitId} from graveyard");

            if (usedPotion)
            {
                int quantity = 1;
                save.PrivateState.Potion = Math.Max(save.PrivateState.Potion - quantity, 0);
            }
            else
            {
                // TODO: Handle the case where potion is not used
            }

            var map = save.Maps[townId];
            var collectedAtTimestamp = TimestampNow();
            int level = 0; // TODO: Set the correct level
            int orientation = 0;

            map.Items.Add(new MapItem(unitId,x,y,orientation,collectedAtTimestamp,level));
        }


        private void HandleGraveyardBuyPotionsCommand(PlayerSave save)
        {
            _logger.LogInformation("Graveyard buy potion");

            var config = GetGameConfig();
            var graveyardPotions = config.Globals.GraveyardPotions;
            int amount = graveyardPotions.Amount;
            int priceCash = graveyardPotions.Price.Cash;

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - priceCash, 0);
            save.PrivateState.Potion += amount;
        }

        private void HandleAdminAddAnimalCommand(PlayerSave save, object[] args)
        {
            string subcatFunc = Convert.ToString(args[0]);
            int toBeAdded = Convert.ToInt32(args[1]);
            var item = GetItemFromSubcatFunctional(subcatFunc);
            _logger.LogInformation($"Added {toBeAdded} {item.Name}");

            var oAnimals = save.PrivateState.ArrayAnimals;
            if (!oAnimals.ContainsKey(subcatFunc))
            {
                oAnimals[subcatFunc] = 0;
            }
            oAnimals[subcatFunc] += toBeAdded;
        }

        private void HandleWinBonusCommand(PlayerSave save, object[] args)
        {
            int coins = Convert.ToInt32(args[0]);
            int townId = Convert.ToInt32(args[1]);
            int hero = Convert.ToInt32(args[2]);
            int claimId = Convert.ToInt32(args[3]);
            int cash = Convert.ToInt32(args[4]);

            _logger.LogInformation("Claiming Win Bonus");

            var map = save.Maps[townId];

            if (cash != 0)
            {
                save.PlayerInfo.Cash += cash;
                _logger.LogInformation($"Added {cash} Cash to player's balance");
            }

            if (coins != 0)
            {
                map.Coins += coins;
                _logger.LogInformation($"Added {coins} Gold to player's balance");
            }

            if (hero != 0)
            {
                var gifts = save.PrivateState.Gifts;
                while (gifts.Count <= hero)
                {
                    gifts.Add(0);
                }
                gifts[hero] += 1;
                _logger.LogInformation($"Added Hero ID={hero}");
            }

            var privateState = save.PrivateState;
            privateState.BonusNextId = claimId + 1;
            privateState.TimestampLastBonus = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }


        private void HandleSelectRiderCommand(PlayerSave save, object[] args)
        {
            int number = Convert.ToInt32(args[0]);
            var pState = save.PrivateState;

            if (number == 1 || number == 2 || number == 3)
            {
                pState.RiderNumber = number;
                _logger.LogInformation($"Rider {number} Selected.");
            }
            else
            {
                pState.RiderNumber = 0;
                pState.RiderStepNumber = 0;
                pState.RiderTimeStamp = -1; // Remove timer
                _logger.LogInformation("Rider reset.");
            }
        }


        private void HandleNextRiderStepCommand(PlayerSave save)
        {
            _logger.LogInformation("Rider step increased.");

            var pState = save.PrivateState;
            pState.RiderStepNumber += 1;
            pState.RiderTimeStamp = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }


        private void HandleRiderBuyStepCashCommand(PlayerSave save, object[] args)
        {
            int price = Convert.ToInt32(args[0]);
            _logger.LogInformation("Buy rider step with cash.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - price, 0);
            save.PrivateState.RiderTimeStamp = -1; // Remove timer
        }


        private void HandleNextDragonStepCommand(PlayerSave save, object[] args)
        {
            _logger.LogInformation("Dragon step increased.");

            var pState = save.PrivateState;
            pState.StepNumber += 1;
            pState.TimeStampTakeCare = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }

        private void HandleNextDragonCommand(PlayerSave save)
        {
            _logger.LogInformation("Dragon step reset and dragonNumber increased.");

            var pState = save.PrivateState;
            pState.StepNumber = 0;
            pState.DragonNumber += 1;
            pState.TimeStampTakeCare = -1; // Remove timer
        }

        private void HandleDragonBuyStepCashCommand(PlayerSave save, object[] args)
        {
            int price = Convert.ToInt32(args[0]);
            _logger.LogInformation("Buy dragon step with cash.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - price, 0);
            save.PrivateState.TimeStampTakeCare = -1; // Remove timer
        }

        private void HandleDeactivateDragonCommand(PlayerSave save)
        {
            _logger.LogInformation("Dragon nest deactivated.");

            var pState = save.PrivateState;
            pState.DragonNestActive = 0;
            pState.StepNumber = 0;
            pState.DragonNumber = 0;
            pState.TimeStampTakeCare = -1; // Remove timer if any
        }

        private void HandleActivateDragonCommand(PlayerSave save, object[] args)
        {
            string currency = Convert.ToString(args[0]);
            _logger.LogInformation("Dragon nest activated.");

            if (currency == "c")
            {
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - 50, 0);
            }
            else if (currency == "g")
            {
                var map = save.Maps[0];
                map.Coins = Math.Max(map.Coins - 100000, 0);
            }

            save.PrivateState.DragonNestActive = 1;
            save.PrivateState.TimeStampTakeCare = -1; // Remove timer if any
        }

        private void HandlePlaceGiftCommand(PlayerSave save, JsonElement[] args)
        {
            var itemId = args[0].GetInt32();
            var x = args[1].GetInt32();
            var y = args[2].GetInt32();
            var townId = args[3].GetInt32(); // Assuming this is correct based on your logic
                                                   // args[4] is unknown and not used in the implementation
            _logger.LogInformation($"Add {itemId} at ({x},{y})");

            var items = save.Maps[townId].Items;
            var orientation = 0; // TODO: Determine the orientation logic
            var collectedAtTimestamp = TimestampNow(); // Assuming a function for current timestamp
            var level = 0;

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

        private void HandleSellGiftCommand(PlayerSave save, JsonElement[] args)
        {
            var itemId = args[0].GetInt32();
            var townId = args[1].GetInt32();
            _logger.LogInformation($"Gift {itemId} sold on town: {townId}");

            var gifts = save.PrivateState.Gifts;
            gifts[itemId]--;

            // Remove excess zeros from the end of the gifts list if necessary
            while (gifts.Count > 0 && gifts[^1] == 0)
            {
                gifts.RemoveAt(gifts.Count - 1);
            }

            // Apply cost if applicable (assuming apply_cost_async is used elsewhere)
            var priceMultiplier = -0.05;
            if (_configFileService.GetItem(itemId).CostType != CostType.Cash)
            {
                ApplyCostAsync(save, itemId, priceMultiplier);
            }
        }

        private void HandleStoreItemCommand(PlayerSave save, JsonElement[] args)
        {
            var x = args[0].GetInt32();
            var y = args[1].GetInt32();
            var townId = args[2].GetInt32();
            var itemId = args[3].GetInt32();
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
            var length = save.PrivateState.Gifts.Count;
            if (length <= itemId)
            {
                for (var i = itemId - length + 1; i > 0; i--)
                {
                    save.PrivateState.Gifts.Add(0);
                }
            }

            // Increment the count of the item_id in gifts
            save.PrivateState.Gifts[itemId]++;
        }

        private void HandleExchangeCashCommand(PlayerSave save, JsonElement[] args)
        {
            var townId = args[0].GetInt32();
            _logger.LogInformation("Exchange cash -> coins.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - 5, 0); // Assuming a function for editing resources is used elsewhere
            save.Maps[townId].Coins += 2500;
        }

        private void HandleNameMapCommand(PlayerSave save, JsonElement[] args)
        {
            var townId = args[0].GetInt32();
            var  newName = args[1].GetString();
            _logger.LogInformation($"Map name changed to '{newName}'.");

            save.PlayerInfo.MapNames[townId] = newName;
        }

        private void HandleExpandCommand(PlayerSave save, JsonElement[] args)
        {
            var landId = args[0].GetInt32();
            var  resource = args[1].GetString();
            var townId = args[2].GetInt32();

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
                var toSubtract = exp.Coins;
                map.Coins = Math.Max(map.Coins - toSubtract, 0);
            }
            else if (resource == "cash")
            {
                var toSubtract = exp.Cash;
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - toSubtract, 0);
            }

            // Add expansion
            expansions.Add(landId);
        }

        private void HandleRTPublishScoreCommand(PlayerSave save, JsonElement[] args)
        {
            var newXp = args[0].GetInt32();
            _logger.LogInformation($"xp set to {newXp}");

            var map = save.Maps[0]; // Assuming xp is general across maps, adjust if necessary
            map.Xp = newXp;
            var levels = _configFileService.Levels;
            var level = levels.FirstOrDefault(_ => _.ExpRequired > newXp);
            map.Level = levels.IndexOf(level);//GetLevelFromXp(newXp);
        }

        private void HandleRTLevelUpCommand(PlayerSave save, JsonElement[] args)
        {
            var newLevel = args[0].GetInt32();
            _logger.LogInformation($"Level Up!: {newLevel}");

            var map = save.Maps[0]; // Assuming xp is general across maps, adjust if necessary
            map.Level = newLevel;

            var currentXp = map.Xp;
            var level = _configFileService.Levels[Math.Max(0, newLevel-1)];
            var minExpectedXp = level.ExpRequired;
            map.Xp = Math.Max(minExpectedXp, currentXp);
        }

        private void HandlePopUnitCommand(PlayerSave save, JsonElement[] args)
        {
            var buildingX = args[0].GetInt32();
            var buildingY = args[1].GetInt32();
            var townId = args[2].GetInt32();
            var unitId = args[3].GetInt32();
            bool placePoppedUnit = args.Length > 4;

            var unitX = 0;
            var unitY = 0;
            var unitFrame = 0; // Assuming unit_frame is an integer, adjust if it's another type

            if (placePoppedUnit)
            {
                unitX = args[4].GetInt32();
                unitY = args[5].GetInt32();
                unitFrame = args[6].GetInt32();
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

        private void HandlePushUnitCommand(PlayerSave save, JsonElement[] args)
        {
            var unitX = args[0].GetInt32();
            var unitY = args[1].GetInt32();
            var unitId = args[2].GetInt32();
            var buildingX = args[3].GetInt32();
            var buildingY = args[4].GetInt32();
            var townId = args[5].GetInt32();

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

        private void HandleRewardMissionCommand(PlayerSave save, JsonElement[] args)
        {
            var townId = args[0].GetInt32();
            var missionId = args[1].GetInt32();

            _logger.LogInformation($"Reward mission {missionId}");

            var missions = _configFileService.Missions
                .FirstOrDefault(_ => _.Id == missionId);

            save.Maps[townId].Coins += missions.Reward;
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

        private void HandleKillCommand(PlayerSave save, JsonElement[] args)
        {
            var x = args[0].GetInt32();
            var y = args[1].GetInt32();
            var id = args[2].GetInt32();
            var townId = args[3].GetInt32();
            var  type = args[4].GetString();

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

        private void HandleSellCommand(PlayerSave save, JsonElement[] args)
        {
            var x = args[0].GetInt32();
            var y = args[1].GetInt32();
            var id = args[2].GetInt32();
            var townId = args[3].GetInt32();
            var dontModifyResources = args[4].GetInt32();
            var  reason = args[5].GetString();

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
            if (dontModifyResources == 0)
            {
                var priceMultiplier = -0.05;
                var item = _configFileService.GetItem(id);
                var costType = item.CostType;
                
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

        private void HandleCollectCommand(PlayerSave save, JsonElement[] args)
        {
            var x = args[0].GetInt32();
            var y = args[1].GetInt32();
            var townId = args[2].GetInt32();
            var id = args[3].GetInt32();
            var numUnitsContainedWhenHarvested = args[4].GetInt32(); // Assuming this affects multiplier logic elsewhere
            var resourceMultiplier = args[5].GetInt32();
            var cashToSubtract = args[6].GetInt32();

            _logger.LogInformation($"Collect {id}");

            var map = save.Maps[townId];
            ApplyCollectAsync(save, id, resourceMultiplier);
            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - cashToSubtract, 0);
        }

        private void HandleCompleteTutorialCommandAsync(PlayerSave save, JsonElement[] args)
        {
            var tutorialStep = args[0].GetInt32();
            if (tutorialStep >= 31)
            {
                save.PlayerInfo.CompletedTutorial = 1;
            }
        }

        private void HandleMoveCommandAsync(PlayerSave save, JsonElement[] args)
        {
            var ix = args[0].GetInt32();
            var iy = args[1].GetInt32();
            var id = args[2].GetInt32();
            var newx = args[3].GetInt32();
            var newy = args[4].GetInt32();
            var frame = args[5].GetInt32(); // You may need to handle this parameter according to your requirements
            var townId = args[6].GetInt32();
            var reason = args[7].GetString(); // "Unitat", "moveTo", "colisio", "MouseUsed"

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

        private void HandleBuyCommandAsync(PlayerSave save, JsonElement[] args)
        {
            
            var id = args[0].GetInt32();
            var x = args[1].GetInt32();
            var y = args[2].GetInt32();
            var frame = args[3].GetInt32(); // TODO ??
            var townId = args[4].GetInt32();
            var dontModifyResources = args[5].GetInt32();
            var priceMultiplier = args[6].GetInt32();
            var type = args[7].GetString();

            _logger.LogInformation($"Add {id} at ({x},{y})");
            var collectedAtTimestamp = TimestampNow();
            var level = 0; // TODO 
            var orientation = 0;
            var map = save.Maps[townId]; // Adjusted from save["maps"][townId] to save.DefaultMap

            if (dontModifyResources == 0)
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
