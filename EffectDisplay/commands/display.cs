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

        public string Description { get; set; } = "automaticly off or on displaying mode";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if ( !Plugin.Instance.Config.IsDatabaseUse )
            {
                response = "the command does not work on this server";
                return false;
            }
            bool flag = !Plugin.data.IsAllow(ply.UserId);
            Plugin.data.IsAllow(ply.UserId, flag);
            if (flag)
            {
                response = "command done\nactive effect showing now:)";
                ply.GameObject.GetComponent<UserEffectDisplayer>().IsEnabled = flag;
                return true;
            }
            else
            {
                response = "done hided effect showing for you";
                return true;
            }
        }
    }
}
