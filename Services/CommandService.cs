using SocialEmpires.Models;
using SocialEmpires.Services.Constants;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SocialEmpires.Services
{
    public class CommandService
    {
        public CommandService() { }

        public async Task HandleCommandsAsync(string userId, JsonNode data)
        {
            var timestamp = data["ts"];
            var firstNumber = data["first_number"];
            var accessToken = data["accessToken"];
            var tries = data["tries"];
            var publishActions = data["publishActions"];
            var commands = data["commands"];

            foreach (var command in commands.AsArray())
            {
                var cmd = command["cmd"].ToString();
                var args = command["args"].AsArray();
                await HandleCommand(userId, cmd, args);
            }
        }

        public async Task HandleCommand(string userId, string cmd, params JsonArray[] args)
        {
            if (cmd == CommandNames.GAME_STATUS)
            {
                
            }
            else if (cmd == CommandNames.BUY)
            {
                var id = args[0].ToString();
                var x = args[1].ToString();
                var y = args[2].ToString();
                var frame = args[3].ToString();
                var townId = ((int)args[4]);
                var dontModifyResources = ((bool)args[5]);
                var priceMultiplier = args[6].ToString();
                var type = args[7].ToString();

                //var map = save.Maps[townId];

                if (!dontModifyResources)
                {
                    //map.Xp += 10;
                }
            }
            else if (cmd == CommandNames.COMPLETE_TUTORIAL)
            {

            }
            else
            {

            }
        }
    }
}
