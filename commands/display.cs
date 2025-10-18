using CommandSystem;
using EffectDisplay.Components;
using EffectDisplay.Features;

using Exiled.API.Features;
using System;

namespace EffectDisplay.commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class display : ICommand
    {
        public string Command { get; set; } = "display";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "The command automatically switches the display mode of active effects.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            UserEffectDisplayer c = ply.GameObject.GetComponent<UserEffectDisplayer>();
            if (ply == null || c == null)
            {
                response = Plugin.Instance.Config.MessageWhenErrorOccurred;
                return false;
            }
            if (!DataBase.IsInitializedAndEnabled)
            {
                response = Plugin.Instance.Config.MessageWnenDataBaseDisabled;
                return false;
            }
            bool flag = !Plugin.data.IsAllow(ply.UserId);
            Plugin.data.IsAllow(ply.UserId, flag);
            c.IsEnabled = flag;
            if (flag)
            {
                response = Plugin.Instance.Config.MessageWhenPlayerEnabled;
                return true;
            }
            else
            {
                response = Plugin.Instance.Config.MessageWhenPlayerDisabled;
                return true;
            }
        }
    }
}
