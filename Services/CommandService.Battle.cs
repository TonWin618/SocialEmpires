using SocialEmpires.Models.Enums;
using SocialEmpires.Models.PlayerSaves;
using System.Text.Json;
using System.Text.Json.Nodes;

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

            var map = save.Maps[townId];

            if (cash != 0)
            {
                AddResource(save, ResourceType.Cash, cash);
            }

            if (coins != 0)
            {
                AddResource(save, ResourceType.Gold, coins);
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
            privateState.TimestampLastBonus = TimestampNow();
        }

        private void HandleSetStrategyCommand(PlayerSave save, JsonElement[] args)
        {
            var strategyType = args[0].GetInt32();
            save.PrivateState.Strategy = strategyType;
        }

        private void HandleResurrectHeroCommand(PlayerSave save, JsonElement[] args)
        {
            var unitId = args[0].GetInt32();
            var x = args[1].GetInt32();
            var y = args[2].GetInt32();
            var townId = args[3].GetInt32();
            bool usedPotion = args.Length > 4 && Convert.ToString(args[4]) == "1";

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
            var graveyardPotions = JsonNode.Parse(_configFileService.GlobalSettings.First(_ => _.Key == "GRAVEYARD_POTIONS").Value);
            var amount = (int)graveyardPotions["amount"];
            var priceCash = (int)graveyardPotions["price"]["c"];

            DeductResource(save, ResourceType.Cash, priceCash);
            save.PrivateState.Potion += amount;
        }
    }
}
