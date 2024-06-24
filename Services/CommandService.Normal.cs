using SocialEmpires.Models;
using SocialEmpires.Models.Enums;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleAddCollectableCommand(PlayerSave save, JsonElement[] args)
        {
            var collectionId = args[0].GetInt32();
            var collectibleId = args[1].GetInt32();
        }

        private void HandleBuySuperOfferPackCommand(PlayerSave save, JsonElement[] args)
        {
            var townId = args[0].GetInt32();
            var superOfferPackId = args[1].GetInt32(); //TODO: assuming this is the super offer pack ID
            var items = args[2].GetString();
            var cashUsed = args[3].GetInt32();

            var map = save.Maps[townId];
            var itemArray = items.Split(',');

            foreach (var item in itemArray)
            {
                var itemId = int.Parse(item);
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

        private void HandleKillCommand(PlayerSave save, JsonElement[] args)
        {
            var x = args[0].GetInt32();
            var y = args[1].GetInt32();
            var id = args[2].GetInt32();
            var townId = args[3].GetInt32();
            var type = args[4].GetString();

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

        private void HandleExchangeCashCommand(PlayerSave save, JsonElement[] args)
        {
            var townId = args[0].GetInt32();
            _logger.LogInformation("Exchange cash -> coins.");
            if (save.PlayerInfo.Cash - 5 < 0)
            {
                //TODO: 
                _logger.LogInformation($"{save.Pid}: an exception occurred during the processing of the ExchangeCash command.");
            }
            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - 5, 0); // Assuming a function for editing resources is used elsewhere
            save.Maps[townId].Coins += 2500;
        }
    }
}
