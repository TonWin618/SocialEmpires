using SocialEmpires.Models;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleSelectRiderCommand(PlayerSave save, JsonElement[] args)
        {
            var number = args[0].GetInt32();
            var pState = save.PrivateState;

            if (number == 1 || number == 2 || number == 3)
            {
                pState.RiderNumber = number;
                _logger.LogInformation($"Rider {number} Selected.");
            }
            else
            {
                pState.RiderNumber = 0;
                pState.RiderStepNumber = 0;
                pState.RiderTimeStamp = -1; // Remove timer
                _logger.LogInformation("Rider reset.");
            }
        }

        private void HandleNextRiderStepCommand(PlayerSave save)
        {
            _logger.LogInformation("Rider step increased.");

            var pState = save.PrivateState;
            pState.RiderStepNumber += 1;
            pState.RiderTimeStamp = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }

        private void HandleRiderBuyStepCashCommand(PlayerSave save, JsonElement[] args)
        {
            var price = args[0].GetInt32();
            _logger.LogInformation("Buy rider step with cash.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - price, 0);
            save.PrivateState.RiderTimeStamp = -1; // Remove timer
        }
    }
}
