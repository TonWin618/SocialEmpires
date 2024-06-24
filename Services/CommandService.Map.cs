using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleOrientCommand(PlayerSave save, JsonElement[] args)
        {
            var x = args[0].GetInt32();
            var y = args[1].GetInt32();
            var newOrientation = args[2].GetInt32();
            var townId = args[3].GetInt32();

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

        private void HandleNameMapCommand(PlayerSave save, JsonElement[] args)
        {
            var townId = args[0].GetInt32();
            var newName = args[1].GetString();
            //TODO: limit length
            _logger.LogInformation($"Map name changed to '{newName}'.");

            save.PlayerInfo.MapNames[townId] = newName;
        }

        private void HandleExpandCommand(PlayerSave save, JsonElement[] args)
        {
            var landId = args[0].GetInt32();
            var resource = args[1].GetString();
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

        private void HandleAdminAddAnimalCommand(PlayerSave save, JsonElement[] args)
        {
            var subcatFunc = args[0].GetString();
            var toBeAdded = args[1].GetInt32();

            var itemsDictSubcatFunctionalToIndex = _configFileService.Items
                .Select((item, index) => new { item.SubcatFunctional, index })
                .ToDictionary(x => x.SubcatFunctional, x => x.index);

            Item? item = null;

            if (itemsDictSubcatFunctionalToIndex.TryGetValue(subcatFunc, out int index))
            {
                item = index >= 0 && index < _configFileService.Items.Count ? _configFileService.Items[index] : null;
            }

            _logger.LogInformation($"Added {toBeAdded} {item.Name}");

            var oAnimals = save.PrivateState.ArrayAnimals;
            if (!oAnimals.ContainsKey(subcatFunc))
            {
                oAnimals[subcatFunc] = 0;
            }
            oAnimals[subcatFunc] += toBeAdded;
        }
    }
}
