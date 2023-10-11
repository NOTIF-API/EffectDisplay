using CommandSystem;
using EffectDisplay.Extension;
using Exiled.API.Features;
using System;
using UnityEngine;

namespace EffectDisplay.Commands
{
    public class Check : ICommand
    {
        public string Command { get; set; } = "check";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "checks for the presence of a component if it seems to you that you do not have it";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "";
                return false;
            }
            if (player.GameObject.GetComponent<EffectReader>() != null & !Main.Instance.DataBaseManager.GetMemberChose(player.UserId))
            {
                response = "your component works successfully (if it doesn't see it, try the turn command, you may have turned it off)";
                return false;
            }
            else
            {
                player.GameObject.AddComponent<EffectReader>();
                response = "we gave you your component for unknown reasons you didn't have it";
                return true;
            }
        }
    }
}
