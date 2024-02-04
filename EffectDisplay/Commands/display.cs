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
    public class display : ICommand
    {
        public string Command { get; set; } = "display";

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
                    bool toset = !Main.Instance.DataBaseManager.GetMemberChose(player.UserId);
                    Main.Instance.DataBaseManager.SaveState(toset, player.UserId);
                    response = "done";
                    return true;
                }
                catch (Exception ex)
                {
                    response = $"error: unkown error detected";
                    Log.Debug($"[EffectDisplay] {ex.Message}");
                    return false;
                }
            }
        }
    }
}
