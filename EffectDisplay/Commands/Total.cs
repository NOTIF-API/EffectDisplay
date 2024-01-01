using CommandSystem;
using EffectDisplay.Extension;
using System;

namespace EffectDisplay.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Total : ICommand
    {
        public string Command { get; set; } = "total";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "displays the number of initialized components";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"now components {EffectReader.TotalEffectReaderComponents}";
            return true;
        }
    }
}
