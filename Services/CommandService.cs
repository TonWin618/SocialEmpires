﻿using SocialEmpires.Models.Enums;
using SocialEmpires.Models.PlayerSaves;
using SocialEmpires.Services.Constants;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public record Command(string Cmd, JsonElement[] Args);

    public partial class CommandService
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
                _logger.LogInformation($"[{userId}][{command.Cmd}]{JsonSerializer.Serialize(command.Args)}");
                await HandleCommand(userId, command.Cmd, command.Args);
            }
        }

        public async Task HandleCommand(string userId, string cmd, params JsonElement[] args)
        {
            var save = await _playerSaveService.GetPlayerSaveAsync(userId);
            if (save == null)
            {
                return;
            }

            switch (cmd)
            {
                case CommandNames.GAME_STATUS:
                    _logger.LogInformation("{message}", string.Join('|', args));
                    break;
                case CommandNames.BUY:
                    HandleBuyCommandAsync(save, args);
                    break;
                case CommandNames.COMPLETE_TUTORIAL:
                    HandleCompleteTutorialCommandAsync(save, args);
                    break;
                case CommandNames.MOVE:
                    HandleMoveCommandAsync(save, args);
                    break;
                case CommandNames.COLLECT:
                    HandleCollectCommand(save, args);
                    break;
                case CommandNames.STORE_ITEM:
                    HandleStoreItemCommand(save, args);
                    break;
                case CommandNames.EXCHANGE_CASH:
                    HandleExchangeCashCommand(save, args);
                    break;
                case CommandNames.NAME_MAP:
                    HandleNameMapCommand(save, args);
                    break;
                case CommandNames.EXPAND:
                    HandleExpandCommand(save, args);
                    break;
                case CommandNames.RT_PUBLISH_SCORE:
                    HandleRTPublishScoreCommand(save, args);
                    break;
                case CommandNames.RT_LEVEL_UP:
                    HandleRTLevelUpCommand(save, args);
                    break;
                case CommandNames.POP_UNIT:
                    HandlePopUnitCommand(save, args);
                    break;
                case CommandNames.PUSH_UNIT:
                    HandlePushUnitCommand(save, args);
                    break;
                case CommandNames.REWARD_MISSION:
                    HandleRewardMissionCommand(save, args);
                    break;
                case CommandNames.COMPLETE_MISSION:
                    HandleCompleteMissionCommand(save, args);
                    break;
                case CommandNames.KILL:
                    HandleKillCommand(save, args);
                    break;
                case CommandNames.SELL:
                    HandleSellCommand(save, args);
                    break;
                case CommandNames.PLACE_GIFT:
                    HandlePlaceGiftCommand(save, args);
                    break;
                case CommandNames.SELL_GIFT:
                    HandleSellGiftCommand(save, args);
                    break;
                case CommandNames.ADD_COLLECTABLE:
                    HandleAddCollectableCommand(save, args);
                    break;
                case CommandNames.START_QUEST:
                    HandleStartQuestCommand(save, args);
                    break;
                case CommandNames.END_QUEST:
                    HandleEndQuestCommand(save, args);
                    break;
                case CommandNames.SET_STRATEGY:
                    HandleSetStrategyCommand(save, args);
                    break;
                case CommandNames.BUY_SUPER_OFFER_PACK:
                    HandleBuySuperOfferPackCommand(save, args);
                    break;
                case CommandNames.NEXT_MONSTER:
                    HandleNextMonsterCommand(save);
                    break;
                case CommandNames.NEXT_MONSTER_STEP:
                    HandleNextMonsterStepCommand(save);
                    break;
                case CommandNames.DESACTIVATE_MONSTER:
                    HandleDesactivateMonsterCommand(save);
                    break;
                case CommandNames.ACTIVATE_MONSTER:
                    HandleActivateMonsterCommand(save, args);
                    break;
                case CommandNames.MONSTER_BUY_STEP_CASH:
                    HandleMonsterBuyStepCashCommand(save, args);
                    break;
                case CommandNames.ORIENT:
                    HandleOrientCommand(save, args);
                    break;
                case CommandNames.RESURRECT_HERO:
                    HandleResurrectHeroCommand(save, args);
                    break;
                case CommandNames.GRAVEYARD_BUY_POTIONS:
                    HandleGraveyardBuyPotionsCommand(save);
                    break;
                case CommandNames.ADMIN_ADD_ANIMAL:
                    HandleAdminAddAnimalCommand(save, args);
                    break;
                case CommandNames.WIN_BONUS:
                    HandleWinBonusCommand(save, args);
                    break;
                case CommandNames.SELECT_RIDER:
                    HandleSelectRiderCommand(save, args);
                    break;
                case CommandNames.NEXT_RIDER_STEP:
                    HandleNextRiderStepCommand(save);
                    break;
                case CommandNames.RIDER_BUY_STEP_CASH:
                    HandleRiderBuyStepCashCommand(save, args);
                    break;
                case CommandNames.NEXT_DRAGON_STEP:
                    HandleNextDragonStepCommand(save, args);
                    break;
                case CommandNames.NEXT_DRAGON:
                    HandleNextDragonCommand(save);
                    break;
                case CommandNames.DRAGON_BUY_STEP_CASH:
                    HandleDragonBuyStepCashCommand(save, args);
                    break;
                case CommandNames.DESACTIVATE_DRAGON:
                    HandleDesactivateDragonCommand(save);
                    break;
                case CommandNames.ACTIVATE_DRAGON:
                    HandleActivateDragonCommand(save, args);
                    break;
                case CommandNames.ACTIVATE:
                    HandleActivateCommand(save, args);
                    break;
                default:
                    _logger.LogWarning("Unknown command: {cmd}", cmd);
                    break;
            }
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

        private static long TimestampNow()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
