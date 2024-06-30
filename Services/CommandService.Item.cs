using SocialEmpires.Models;
using SocialEmpires.Models.Enums;
using SocialEmpires.Models.PlayerSaves;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleSellCommand(PlayerSave save, JsonElement[] args)
        {
            var x = args[0].GetInt32();
            var y = args[1].GetInt32();
            var id = args[2].GetInt32();
            var townId = args[3].GetInt32();
            var dontModifyResources = args[4].GetInt32();
            var reason = args[5].GetString();

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

        private void HandleActivateCommand(PlayerSave save, JsonElement[] args)
        {
            var buildingX = args[0].GetInt32();
            var buildingY = args[1].GetInt32();
            var townId = args[2].GetInt32();
            var buildingId = args[3].GetInt32();
            var collectPeriod = args[4].GetInt32();

            var item = save.Maps[townId].Items.First(_ => _.X == buildingX && _.Y == buildingY);
            if (item == null) 
            {
                return;
            }
            item.Timestamp = TimestampNow();

            if (item.Attributes!.ContainsKey("cp"))
            {
                item.Attributes["cp"] = collectPeriod;
            }
            else
            {
                item.Attributes!.Add("cp", collectPeriod);
            }
        }

        private void HandlePopUnitCommand(PlayerSave save, JsonElement[] args)
        {
            var buildingX = args[0].GetInt32();
            var buildingY = args[1].GetInt32();
            var townId = args[2].GetInt32();
            var unitId = args[3].GetInt32();
            var placePoppedUnit = args.Length > 4;

            var unitX = 0;
            var unitY = 0;
            var unitFrame = 0;

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
                        item.Units.Remove(unitId);
                        break;
                    }
                }
            }

            if (placePoppedUnit)
            {
                // Spawn unit outside
                var collectedAtTimestamp = TimestampNow();
                var level = 0; // TODO: Adjust level logic as needed
                var orientation = 0;

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
    }
}
