using SocialEmpires.Models;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        private void HandleNextMonsterCommand(PlayerSave save)
        {
            _logger.LogInformation("Monster Step reset and Monster Number increased.");

            var privateState = save.PrivateState;
            privateState.StepMonsterNumber = 0;
            privateState.MonsterNumber += 1;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer
        }

        private void HandleNextMonsterStepCommand(PlayerSave save)
        {
            _logger.LogInformation("Monster Step increased.");

            var privateState = save.PrivateState;
            privateState.StepMonsterNumber += 1;
            privateState.TimeStampTakeCareMonster = TimestampNow(); // Assuming TimestampNow() returns the current timestamp
        }

        private void HandleDesactivateMonsterCommand(PlayerSave save)
        {
            _logger.LogInformation("Monster nest deactivated.");

            var privateState = save.PrivateState;
            privateState.MonsterNestActive = 0;
            privateState.StepMonsterNumber = 0;
            privateState.MonsterNumber = 0;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer if any
        }

        private void HandleActivateMonsterCommand(PlayerSave save, JsonElement[] args)
        {
            var currency = args[0].GetString();
            _logger.LogInformation("Monster nest activated.");

            if (currency == "c")
            {
                save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - 50, 0);
            }
            else if (currency == "g")
            {
                var map = save.Maps[0];
                map.Coins = Math.Max(map.Coins - 100000, 0);
            }

            var privateState = save.PrivateState;
            privateState.MonsterNestActive = 1;
            privateState.TimeStampTakeCareMonster = -1; // Remove timer if any
        }

        private void HandleMonsterBuyStepCashCommand(PlayerSave save, JsonElement[] args)
        {
            var price = args[0].GetInt32();
            _logger.LogInformation("Buy monster step with cash.");

            save.PlayerInfo.Cash = Math.Max(save.PlayerInfo.Cash - price, 0);
            save.PrivateState.TimeStampTakeCareMonster = -1; // Remove timer
        }
    }
}
