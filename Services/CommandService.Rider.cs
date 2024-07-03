using SocialEmpires.Models.PlayerSaves;
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
            }
            else
            {
                pState.RiderNumber = 0;
                pState.RiderStepNumber = 0;
                pState.RiderTimeStamp = -1; // Remove timer
            }
        }

        private void HandleNextRiderStepCommand(PlayerSave save)
        {
            var pState = save.PrivateState;
            pState.RiderStepNumber += 1;
            pState.RiderTimeStamp = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }

        private void HandleRiderBuyStepCashCommand(PlayerSave save, JsonElement[] args)
        {
            var price = args[0].GetInt32();

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - price, 0);
            save.PrivateState.RiderTimeStamp = -1; // Remove timer
        }
    }
}
