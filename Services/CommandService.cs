using SocialEmpires.Models.Enums;
using SocialEmpires.Models.PlayerSaves;
using SocialEmpires.Services.Constants;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public record Command(string Cmd, JsonElement[] Args);

    public partial class CommandService
    {
        private readonly ConfigService _configFileService;
        private readonly PlayerSaveService _playerSaveService;
        private readonly ILogger<CommandService> _logger;

        public CommandService(
            ConfigService configFileService,
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
            var collect = item.Collect * multiplier;
            //TODO: deduct wood when collect from farmland
            AddResource(save, (ResourceType)item.CollectType.First(), (int)collect);

            var collectXp = item.CollectXp;
            save.DefaultMap.Xp += collectXp;
        }

        private void ApplyCost(PlayerSave save, int id, double multiplier)
        {
            var item = _configFileService.GetItem(id);
            if (item == null)
            {
                return;
            }

            var cost = multiplier * item.Cost;
            //TODO: cost food when buy unit by gold
            DeductResource(save, (ResourceType)item.CostType.First(), (int)cost);
        }

        private void ApplyCollectXp(PlayerSave save, int id)
        {
            var item = _configFileService.GetItem(id);
            if (item == null)
            {
                return;
            }
            var collectXp = item.CollectXp;
            save.DefaultMap.Xp += collectXp;
        }

        private void DeductResource(PlayerSave save, ResourceType costType, int quantity)
        {
            if(quantity < 0)
            {
                _logger.LogWarning("quantity must be greater than or equal to 0. ");
                return;
            }

            switch (costType)
            {
                case ResourceType.Wood:
                    if(save.DefaultMap.Wood >= quantity)
                    {
                        save.DefaultMap.Wood -= quantity;
                    }
                    else
                    {
                        _logger.LogWarning("Don't have enough wood. ");
                    }
                    break;
                case ResourceType.Gold:
                    if (save.DefaultMap.Coins >= quantity)
                    {
                        save.DefaultMap.Coins -= quantity;
                    }
                    else
                    {
                        _logger.LogWarning("Don't have enough gold. ");
                    }
                    break;
                case ResourceType.Cash:
                    if (save.PlayerInfo.Cash >= quantity)
                    {
                        save.PlayerInfo.Cash -= quantity;
                    }
                    else
                    {
                        _logger.LogWarning("Don't have enough cash. ");
                    }
                    break;
                case ResourceType.Stone:
                    if (save.DefaultMap.Stone >= quantity)
                    {
                        save.DefaultMap.Stone -= quantity;
                    }
                    else
                    {
                        _logger.LogWarning("Don't have enough stone. ");
                    }
                    break;
                case ResourceType.Food:
                    if (save.DefaultMap.Food >= quantity)
                    {
                        save.DefaultMap.Food -= quantity;
                    }
                    else
                    {
                        _logger.LogWarning("Don't have enough food. ");
                    }
                    break;
            }
        }

        private void AddResource(PlayerSave save, ResourceType costType, int quantity)
        {
            if (quantity < 0)
            {
                _logger.LogWarning("quantity must be greater than or equal to 0. ");
                return;
            }

            switch (costType)
            {
                case ResourceType.Wood:
                    save.DefaultMap.Wood += quantity;
                    break;
                case ResourceType.Gold:
                    save.DefaultMap.Coins += quantity;
                    break;
                case ResourceType.Cash:
                    save.PlayerInfo.Cash += quantity;
                    break;
                case ResourceType.Stone:
                    save.DefaultMap.Stone += quantity;
                    break;
                case ResourceType.Food:
                    save.DefaultMap.Food += quantity;
                    break;
            }
        }

        private static long TimestampNow()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
