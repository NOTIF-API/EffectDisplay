using CommandSystem;
using EffectDisplay.Components;
using EffectDisplay.Features;
using EffectDisplay.Features.Sereliazer;

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
            if (ply == null)
            {
                response = Plugin.Instance.Config.MessageWhenErrorOccurred;
                return true;
            }
            if (!Plugin.Instance.Config.DataBaseEnabled)
            {
                response = Plugin.Instance.Config.MessageWnenDataBaseDisabled;
                return true;
            }
            else
            {
                User user = null;
                if (!DataBase.TryGetData(ply.UserId, out user))
                {
                    user = new User()
                    {
                        UserId = ply.UserId
                    };
                }
                user.IsAllow = !user.IsAllow;
                DataBase.TryUpdate(user);
                if (!ply.GameObject.TryGetComponent(out UserEffectDisplayer component))
                {
                    response = Plugin.Instance.Config.MessageWhenErrorOccurred;
                    return true;
                }
                component.IsEnabled = user.IsAllow;
                response = user.IsAllow ? Plugin.Instance.Config.MessageWhenPlayerEnabled : Plugin.Instance.Config.MessageWhenPlayerDisabled;
                return true;
            }
        }
    }
}