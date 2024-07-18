using CustomPlayerEffects;
using EffectDisplay.Extensions;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EffectDisplay.Components
{
    public class UserEffectDisplayer: MonoBehaviour
    {
        private Player player;
        /// <summary>
        /// Stores the current process for future stopping
        /// </summary>
        private CoroutineHandle Current;

        private bool Enabled = true;
        /// <summary>
        /// Will the display of effects be enabled?
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return Enabled;
            }
            set
            {
                Enabled = value;
                Timing.KillCoroutines(Current);
                if (value)
                {
                    Current = Timing.RunCoroutine(PlayerEffectShower(player));
                }
            }
        }

        private string Category(EffectType effectType)
        {
            if (effectType == EffectType.Scp207 | effectType == EffectType.AntiScp207)
            {
                return Plugin.Instance.Config.EffectLine["Mixed"];
            }

            if (effectType.IsHarmful() || effectType.IsNegative())
            {
                return Plugin.Instance.Config.EffectLine["Negative"];
            }
            else
            {
                return Plugin.Instance.Config.EffectLine["Positive"];
            }
        }

        private void Awake()
        {
            player = Player.Get(gameObject);
            if (!player.IsAllow())
            {
                this.IsEnabled = false;
                return;
            }
            else
            {
                Timing.RunCoroutine(PlayerEffectShower(player));
            }
        }

        private IEnumerator<float> PlayerEffectShower(Player ply)
        {
            for (; ; )
            {
                // check whether it is necessary to calculate active effects for the user at the current moment
                if (ply.IsAlive | !Plugin.Instance.Config.IgnoredRoles.Contains(ply.Role.Type) | ply.ActiveEffects.Count() != 0)
                {
                    // we check whether the effects display has been disabled for the user or whether the player has disconnected
                    if (ply == null | !ply.IsConnected | !this.Enabled)
                    {
                        Destroy(this);
                        break;
                    }
                    else
                    {
                        StringBuilder output = new StringBuilder();
                        output.Append("\n\n\n\n");
                        foreach (StatusEffectBase type in ply.ActiveEffects)
                        {
                            string name = Plugin.Instance.Config.GetTranslation(type.GetEffectType());
                            string line = Category(type.GetEffectType());
                            line = type.Duration == 0 ? line.Replace("%time%", "inf") : line.Replace("%time%", ((int)type.TimeLeft).ToString());
                            line = line.Replace("%intensity%", type.Intensity.ToString());
                            line = line.Replace("%type%", name);
                            output.AppendLine(line);
                        }
                        Log.Debug($"{nameof(PlayerEffectShower)} Try to show hint for ply {ply.Nickname}");
                        ply.ShowHint(output.ToString(), 1);
                    }
                }
                yield return Timing.WaitForSeconds(0.9f);
            }
        }
    }
}
