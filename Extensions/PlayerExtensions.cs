using EffectDisplay.Components;
using Exiled.API.Features;

namespace EffectDisplay.Extensions
{
    public static class PlayerExtensions
    {
        /// <summary>
        /// Returns the Allow parameter that determines the permission to show the display
        /// </summary>
        /// <param name="player"></param>
        public static bool GetIsAllow(this Player player) => Plugin.data.IsAllow(player.UserId);
        /// <summary>
        /// Allows you to change the value of the IsAllow parameter
        /// </summary>
        public static void SetIsAllow(this Player player, bool IsAllow) => Plugin.data.IsAllow(player.UserId, IsAllow);
    }
}
