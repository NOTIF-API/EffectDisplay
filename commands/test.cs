using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;

using EffectDisplay.Features;

using Exiled.API.Features;

using MEC;

namespace EffectDisplay.commands
{
    // I’m trying to understand why the text is not displayed completely and not all the effects are displayed, but I see that everything works here and the problem is not in reflection
    /*
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class test : ICommand
    {
        public string Command { get; set; } = "meowtest";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "test ow meowww";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            MeowHintManager hintlol = new MeowHintManager();
            StringBuilder pdk = new StringBuilder();
            pdk.AppendLine(arguments.At(0));
            pdk.AppendLine(arguments.At(7));
            hintlol.SetText(pdk.ToString());
            hintlol.SetFont(int.Parse(arguments.At(1)));
            hintlol.SetVerticalAligment(arguments.At(2));
            hintlol.SetHorizontalAligment(arguments.At(3));
            hintlol.SetId(arguments.At(4));
            hintlol.SetXCoordinate(int.Parse((string)arguments.At(5)));
            hintlol.SetYCoordinates(int.Parse(((string)arguments.At(6))));
            MeowHintManager.AddHint(Player.Get(sender), hintlol.GetHintProcessionObject());
            Timing.CallDelayed(5f, () => {
                MeowHintManager.RemoveHint(Player.Get(sender), hintlol.GetHintProcessionObject());
            });
            response = "lol";
            return true;
        }
    }*/
}
