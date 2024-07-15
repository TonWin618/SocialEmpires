using SocialEmpires.Models.Enums;
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
                pState.RiderTimeStamp = -1;
            }
        }

        private void HandleNextRiderStepCommand(PlayerSave save)
        {
            var pState = save.PrivateState;
            pState.RiderStepNumber += 1;
            pState.RiderTimeStamp = TimestampNow();
        }

        private void HandleRiderBuyStepCashCommand(PlayerSave save, JsonElement[] args)
        {
            var price = args[0].GetInt32();
            DeductResource(save, ResourceType.Cash, price);
            save.PrivateState.RiderTimeStamp = -1;
        }
    }
}
