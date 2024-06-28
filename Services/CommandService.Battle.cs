using SocialEmpires.Models;
using SocialEmpires.Models.PlayerSaves;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleWinBonusCommand(PlayerSave save, JsonElement[] args)
        {
            var coins = args[0].GetInt32();
            var townId = args[1].GetInt32();
            var hero = args[2].GetInt32();
            var claimId = args[3].GetInt32();
            var cash = args[4].GetInt32();

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

        private void HandleSetStrategyCommand(PlayerSave save, JsonElement[] args)
        {
            var strategyType = args[0].GetInt32();
            save.PrivateState.Strategy = strategyType;
            _logger.LogInformation($"Set defense strategy type to {strategyType}");
        }

        private void HandleResurrectHeroCommand(PlayerSave save, JsonElement[] args)
        {
            var unitId = args[0].GetInt32();
            var x = args[1].GetInt32();
            var y = args[2].GetInt32();
            var townId = args[3].GetInt32();
            bool usedPotion = args.Length > 4 && Convert.ToString(args[4]) == "1";

            _logger.LogInformation($"Resurrect {unitId} from graveyard");

            if (usedPotion)
            {
                var quantity = 1;
                save.PrivateState.Potion = Math.Max(save.PrivateState.Potion - quantity, 0);
            }
            else
            {
                // TODO: Handle the case where potion is not used
            }

            var map = save.Maps[townId];
            var collectedAtTimestamp = TimestampNow();
            var level = 0; // TODO: Set the correct level
            var orientation = 0;

            map.Items.Add(new MapItem(unitId, x, y, orientation, collectedAtTimestamp, level));
        }

        private void HandleGraveyardBuyPotionsCommand(PlayerSave save)
        {
            _logger.LogInformation("Graveyard buy potion");

            var graveyardPotions = _configFileService.Globals.GetProperty("GRAVEYARD_POTIONS");
            var amount = graveyardPotions.GetProperty("amount").GetInt32();
            var priceCash = graveyardPotions.GetProperty("price").GetProperty("c").GetInt32();

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - priceCash, 0);
            save.PrivateState.Potion += amount;
        }
    }
}
