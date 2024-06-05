using CommandSystem;
using EffectDisplay.Components;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectDisplay.commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class display : ICommand
    {
        public string Command { get; set; } = "display";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "automaticly off or on displaying mode";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            bool flag = !Plugin.data.IsAllow(ply.UserId);
            Plugin.data.SetAllowTo(ply.UserId, flag);
            if (flag)
            {
                response = "command done\nactive effect showing now:)";
                ply.GameObject.AddComponent<UserEffectDisplayer>();
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
