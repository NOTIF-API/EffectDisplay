using CommandSystem;
using System;

namespace EffectDisplay.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class EfParent : ParentCommand
    {
        public override string Command { get; } = "EffectDisplay";

        public override string[] Aliases { get; } = { "efd" };

        public override string Description { get; } = "controlling the display of active effects for the player";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new display());
            RegisterCommand(new Check());
            RegisterCommand(new display());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "EffectDisplay turn | check | total";
            return true;
        }
    }
}
