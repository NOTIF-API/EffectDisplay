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

        private string Category(StatusEffectBase statusEffectBase)
        {
            if (statusEffectBase.Classification == StatusEffectBase.EffectClassification.Mixed)
            {
                return Plugin.Instance.Config.EffectLine["Mixed"];
            }
            if (statusEffectBase.Classification == StatusEffectBase.EffectClassification.Negative)
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
            Log.Debug($"{nameof(Awake)} Initing component");
            player = Player.Get(gameObject);
            Log.Debug(player);
            if (player.IsAllow())
            {
                Log.Debug("Starting Corountine");
                Timing.RunCoroutine(PlayerEffectShower(player));
            }
            else
            {
                this.IsEnabled = false;
                Timing.KillCoroutines(this.Current);
                return;
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
                        Log.Debug("Destroy components");
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
                            string line = $"{Plugin.Instance.Config.HintLocation}{Category(type)}</align>";
                            // Current end time line
                            line = type.Duration == 0 ? line.Replace("%time%", "inf") : line.Replace("%time%", ((int)type.TimeLeft).ToString());
                            // Effect duration total
                            line = type.Duration == 0 ? line.Replace("%duration%", "inf") : line.Replace("%duration%", type.Duration.ToString());
                            line = line.Replace("%intensity%", type.Intensity.ToString());
                            line = line.Replace("%effect%", name);
                            output.AppendLine(line);
                        }
                        ply.ShowHint(output.ToString(), 1);
                    }
                }
                yield return Timing.WaitForSeconds(0.9f);
            }
        }
    }
}
