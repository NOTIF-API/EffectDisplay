using EffectDisplay.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectDisplay.API
{
    public class Features
    {
        /// <summary>
        /// Does it allow the player to see the status of effects?
        /// </summary>
        /// <param name="userid">user id</param>
        /// <returns>true if allow</returns>
        public static bool IsEffectShowAllow(string userid)
        {
            bool IsEnabled = !Main.Instance.DataBaseManager.GetMemberChose(userid);
            return IsEnabled;
        }
        /// <summary>
        /// Does it allow the player to see the status of effects?
        /// </summary>
        /// <param name="player"><see cref="Player"/> object</param>
        /// <returns></returns>
        public static bool IsEffectShowAllow(Player player)
        {
            if (player == null) return true;
            else
            {
                bool IsEnabled = !Main.Instance.DataBaseManager.GetMemberChose(player.UserId);
                return IsEnabled;
            }
        }
        /// <summary>
        /// Allows you to save a player in the database with a preset argument; if there is a user, then simply changes it
        /// </summary>
        /// <param name="player">The <see cref="Player"/> for which the action is applied</param>
        /// <param name="IsShow">The <see cref="bool"/> value for set</param>
        public static void SaveMemberWithArg(Player player, bool IsShow) => Main.Instance.DataBaseManager.SaveState(!IsShow, player.UserId);
    }
}
