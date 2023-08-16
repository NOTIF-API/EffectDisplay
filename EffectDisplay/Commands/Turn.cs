using CommandSystem;
using Exiled.API.Features;
using System;

namespace EffectDisplay.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Turn : ICommand
    {
        public string Command { get; set; } = "turn";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "turn on or turn off display during the round";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "";
                return false;
            }
            else
            {
                if (arguments.At(0) == "off")
                {
                    response = "done";
                    player.SessionVariables.Add("EDoff", null);
                    return true;
                }
                if (arguments.At(0) == "on")
                {
                    response = "done";
                    player.SessionVariables.Remove("EDoff");
                    return true;
                }
                else
                {
                    response = "your argument not off or on!";
                    return false;
                }
            }
        }
    }
}
