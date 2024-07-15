using SocialEmpires.Models.Enums;
using SocialEmpires.Models.PlayerSaves;
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
            var newName = args[1].GetString()!;
            //TODO: limit length

            save.PlayerInfo.MapNames[townId] = newName;
        }

        private void HandleExpandCommand(PlayerSave save, JsonElement[] args)
        {
            var landId = args[0].GetInt32();
            var resource = args[1].GetString();
            var townId = args[2].GetInt32();

            var map = save.Maps[townId];
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
                DeductResource(save, ResourceType.Gold, exp.Coins);
            }
            else if (resource == "cash")
            {
                DeductResource(save, ResourceType.Cash, exp.Cash);
            }

            // Add expansion
            expansions.Add(landId);
        }

        private void HandleAdminAddAnimalCommand(PlayerSave save, JsonElement[] args)
        {
            var subcatFunc = args[0].GetInt32().ToString();
            var toBeAdded = args[1].GetInt32();

            var oAnimals = save.PrivateState.ArrayAnimals;
            if (!oAnimals.ContainsKey(subcatFunc))
            {
                oAnimals[subcatFunc] = 0;
            }
            oAnimals[subcatFunc] += toBeAdded;
        }
    }
}
