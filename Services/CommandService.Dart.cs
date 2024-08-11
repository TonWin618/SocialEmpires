using SocialEmpires.Models.PlayerSaves;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public partial class CommandService
    {
        public void HandleDartsResetCommand(PlayerSave save, JsonElement[] args)
        {
            var randomSeed = args[0].GetInt32();

            save.PrivateState.TimeStampDartsReset = TimestampNow();
            save.PrivateState.TimeStampDartsNewFree = TimestampNow();
            save.PrivateState.DartsBalloonsShot = [];
            save.PrivateState.DartsHasFree = true;
            save.PrivateState.DartsGotExtra = false;
            save.PrivateState.DartsRandomSeed = randomSeed;
        }

        public void HandleDartsShootBallonCommand(PlayerSave save, JsonElement[] args)
        {
            var ballonShot = args[0].GetInt32();
            var dartsHasFree = args[1].GetInt32();
            var gotExtraInThisThrow = args[2].GetInt32();

            save.PrivateState.DartsBalloonsShot.Add(ballonShot);
            save.PrivateState.DartsHasFree = dartsHasFree == 1;
            save.PrivateState.DartsGotExtra = gotExtraInThisThrow == 1;
        }

        public void HandleDartsNewFreeCommand(PlayerSave save, JsonElement[] args)
        {
            save.PrivateState.TimeStampDartsNewFree = TimestampNow();
            save.PrivateState.DartsHasFree = true;
        }

        public void HandleStoreAddItemsCommand(PlayerSave save, JsonElement[] args)
        {
            var items = args[0].GetString();
            save.DefaultMap.
        }
    }
}
