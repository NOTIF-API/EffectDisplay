using CommandSystem;
using EffectDisplay.Components;
using Exiled.API.Features;
using System;

namespace EffectDisplay.commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class display : ICommand
    {
        public string Command { get; set; } = "display";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public bool SanitizeResponse => true;

        public string Description { get; set; } = "The command automatically switches the display mode of active effects.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if ( !Plugin.Instance.Config.DataBaseEnabled )
            {
                response = "The specified server does not have this function.";
                return false;
            }
            bool flag = !Plugin.data.IsAllow(ply.UserId);
            Plugin.data.IsAllow(ply.UserId, flag);
            ply.GameObject.GetComponent<UserEffectDisplayer>().IsEnabled = flag;
            if (flag)
            {
                response = "The command was completed successfully.\nDisplay of active effects will be enabled for you.";
                return true;
            }
            else
            {
                response = "The command was completed successfully.\nDisplay of active effects will be disabled for you.";
                return true;
            }
        }
    }
}
