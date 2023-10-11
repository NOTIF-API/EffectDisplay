using CommandSystem;
using EffectDisplay.Extension;
using Exiled.API.Features;
using EffectDisplay.Features;
using EffectDisplay.Features.Serelization;
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
                try
                {
                    if (arguments.At(0) == "off")
                    {
                        response = "now you don't see active effects in front of the screen";
                        Main.Instance.DataBaseManager.SaveState(true, player.UserId);
                        player.GameObject.GetComponent<EffectReader>().StatUpdate(true);
                        return true;
                    }
                    if (arguments.At(0) == "on")
                    {
                        response = "now you see active effects in front of the screen";
                        Main.Instance.DataBaseManager.SaveState(false, player.UserId);
                        player.GameObject.GetComponent<EffectReader>().StatUpdate(false);
                        return true;
                    }
                    else
                    {
                        response = "your argument not off or on!";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    response = $"error: {ex.Message}";
                    return false;
                }
            }
        }
    }
}
