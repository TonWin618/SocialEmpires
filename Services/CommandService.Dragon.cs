using SocialEmpires.Models;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleNextDragonStepCommand(PlayerSave save, JsonElement[] args)
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

        private void HandleDragonBuyStepCashCommand(PlayerSave save, JsonElement[] args)
        {
            var price = args[0].GetInt32();
            _logger.LogInformation("Buy dragon step with cash.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - price, 0);
            save.PrivateState.TimeStampTakeCare = -1; // Remove timer
        }

        private void HandleDesactivateDragonCommand(PlayerSave save)
        {
            _logger.LogInformation("Dragon nest deactivated.");

            var pState = save.PrivateState;
            pState.DragonNestActive = 0;
            pState.StepNumber = 0;
            pState.DragonNumber = 0;
            pState.TimeStampTakeCare = -1; // Remove timer if any
        }

        private void HandleActivateDragonCommand(PlayerSave save, JsonElement[] args)
        {
            var currency = args[0].GetString();
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
    }
}
