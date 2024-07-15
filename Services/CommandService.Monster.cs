using SocialEmpires.Models.PlayerSaves;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleNextMonsterCommand(PlayerSave save)
        {
            var privateState = save.PrivateState;
            privateState.StepMonsterNumber = 0;
            privateState.MonsterNumber += 1;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer
        }

        private void HandleNextMonsterStepCommand(PlayerSave save)
        {
            var privateState = save.PrivateState;
            privateState.StepMonsterNumber += 1;
            privateState.TimeStampTakeCareMonster = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }

        private void HandleDesactivateMonsterCommand(PlayerSave save)
        {
            var privateState = save.PrivateState;
            privateState.MonsterNestActive = 0;
            privateState.StepMonsterNumber = 0;
            privateState.MonsterNumber = 0;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer if any
        }

        private void HandleActivateMonsterCommand(PlayerSave save, JsonElement[] args)
        {
            var currency = args[0].GetString();

            if (currency == "c")
            {
                DeductResource(save, Models.Enums.ResourceType.Cash, 50);
            }
            else if (currency == "g")
            {
                DeductResource(save, Models.Enums.ResourceType.Gold, 100000);
            }

            var privateState = save.PrivateState;
            privateState.MonsterNestActive = 1;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer if any
        }

        private void HandleMonsterBuyStepCashCommand(PlayerSave save, JsonElement[] args)
        {
            var price = args[0].GetInt32();

            DeductResource(save, Models.Enums.ResourceType.Cash, price);

            save.PrivateState.TimeStampTakeCareMonster = -1; // Remove timer
        }
    }
}
