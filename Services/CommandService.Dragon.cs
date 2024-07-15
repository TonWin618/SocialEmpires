using SocialEmpires.Models.Enums;
using SocialEmpires.Models.PlayerSaves;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleNextDragonStepCommand(PlayerSave save, JsonElement[] args)
        {
            var pState = save.PrivateState;
            pState.StepNumber += 1;
            pState.TimeStampTakeCare = TimestampNow();
        }

        private void HandleNextDragonCommand(PlayerSave save)
        {
            var pState = save.PrivateState;
            pState.StepNumber = 0;
            pState.DragonNumber += 1;
            pState.TimeStampTakeCare = -1;
        }

        private void HandleDragonBuyStepCashCommand(PlayerSave save, JsonElement[] args)
        {
            var price = args[0].GetInt32();

            DeductResource(save, ResourceType.Cash, price);
            save.PrivateState.TimeStampTakeCare = -1;
        }

        private void HandleDesactivateDragonCommand(PlayerSave save)
        {
            var pState = save.PrivateState;
            pState.DragonNestActive = 0;
            pState.StepNumber = 0;
            pState.DragonNumber = 0;
            pState.TimeStampTakeCare = -1;
        }

        private void HandleActivateDragonCommand(PlayerSave save, JsonElement[] args)
        {
            var currency = args[0].GetString();

            if ((ResourceType)currency!.First() == ResourceType.Cash)
            {
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - 50, 0);
            }
            else if ((ResourceType)currency!.First() == ResourceType.Gold)
            {
                var map = save.Maps[0];
                map.Coins = Math.Max(map.Coins - 100000, 0);
            }

            save.PrivateState.DragonNestActive = 1;
            save.PrivateState.TimeStampTakeCare = -1;
        }
    }
}
