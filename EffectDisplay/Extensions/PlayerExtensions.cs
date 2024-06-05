using Exiled.API.Features;

namespace EffectDisplay.Extensions
{
    public static class PlayerExtensions
    {
        /// <summary>
        /// determines the user's choice of displaying effect status
        /// </summary>
        /// <param name="player"></param>
        /// <returns>true if is allow or false</returns>
        public static bool DataChoise(this Player player) => Plugin.data.IsAllow(player.UserId);

    }
}
