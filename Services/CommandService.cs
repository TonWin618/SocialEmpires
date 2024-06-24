using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models.Enums;
using SocialEmpires.Services.Constants;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public record Command(string Cmd, JsonElement[] Args);

    public partial class CommandService
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
            if (save == null)
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
            else if (cmd == CommandNames.PLACE_GIFT)
            {
                HandlePlaceGiftCommand(save, args);
            }
            else if (cmd == CommandNames.SELL_GIFT)
            {
                HandleSellGiftCommand(save, args);
            }
            else if (cmd == CommandNames.ADD_COLLECTABLE)
            {
                HandleAddCollectableCommand(save, args);
            }
            else if (cmd == CommandNames.START_QUEST)
            {
                HandleStartQuestCommand(save, args);
            }
            else if (cmd == CommandNames.END_QUEST)
            {
                HandleEndQuestCommand(save, args);
            }
            else if (cmd == CommandNames.SET_STRATEGY)
            {
                HandleSetStrategyCommand(save, args);
            }
            else if (cmd == CommandNames.BUY_SUPER_OFFER_PACK)
            {
                HandleBuySuperOfferPackCommand(save, args);
            }
            else if (cmd == CommandNames.NEXT_MONSTER)
            {
                HandleNextMonsterCommand(save);
            }
            else if (cmd == CommandNames.NEXT_MONSTER_STEP)
            {
                HandleNextMonsterStepCommand(save);
            }
            else if (cmd == CommandNames.DESACTIVATE_MONSTER)
            {
                HandleDesactivateMonsterCommand(save);
            }
            else if (cmd == CommandNames.ACTIVATE_MONSTER)
            {
                HandleActivateMonsterCommand(save, args);
            }
            else if (cmd == CommandNames.MONSTER_BUY_STEP_CASH)
            {
                HandleMonsterBuyStepCashCommand(save, args);
            }
            else if (cmd == CommandNames.ORIENT)
            {
                HandleOrientCommand(save, args);
            }
            else if (cmd == CommandNames.RESURRECT_HERO)
            {
                HandleResurrectHeroCommand(save, args);
            }
            else if (cmd == CommandNames.GRAVEYARD_BUY_POTIONS)
            {
                HandleGraveyardBuyPotionsCommand(save);
            }
            else if (cmd == CommandNames.ADMIN_ADD_ANIMAL)
            {
                HandleAdminAddAnimalCommand(save, args);
            }
            else if (cmd == CommandNames.WIN_BONUS)
            {
                HandleWinBonusCommand(save, args);
            }
            else if (cmd == CommandNames.SELECT_RIDER)
            {
                HandleSelectRiderCommand(save, args);
            }
            else if (cmd == CommandNames.NEXT_RIDER_STEP)
            {
                HandleNextRiderStepCommand(save);
            }
            else if (cmd == CommandNames.RIDER_BUY_STEP_CASH)
            {
                HandleRiderBuyStepCashCommand(save, args);
            }
            else if (cmd == CommandNames.NEXT_DRAGON_STEP)
            {
                HandleNextDragonStepCommand(save, args);
            }
            else if (cmd == CommandNames.NEXT_DRAGON)
            {
                HandleNextDragonCommand(save);
            }
            else if (cmd == CommandNames.DRAGON_BUY_STEP_CASH)
            {
                HandleDragonBuyStepCashCommand(save, args);
            }
            else if (cmd == CommandNames.DESACTIVATE_DRAGON)
            {
                HandleDesactivateDragonCommand(save);
            }
            else if (cmd == CommandNames.ACTIVATE_DRAGON)
            {
                HandleActivateDragonCommand(save, args);
            }
            else
            {
                _logger.LogWarning($"Unknown command: {cmd}");
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

        private long TimestampNow()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
