using EffectDisplay.Extensions;

using Exiled.API.Extensions;
using Exiled.API.Features;

using MEC;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                Log.Debug($"{nameof(IsEnabled)}.Set: {Enabled} -> {value}.");
                Enabled = value;
                Timing.KillCoroutines(Current);
                if (value)
                {
                    Current = Timing.RunCoroutine(PlayerEffectShower());
                }
            }
        }

        private void Awake()
        {
            Log.Debug($"{nameof(Awake)}: Initing component.");
            player = Player.Get(gameObject);
            Log.Debug($"{nameof(Awake)}: {player.Nickname} Awake and adding component handling.");
            if (player.IsAllow())
            {
                Log.Debug($"{nameof(Awake)}: Starting Corountine.");
                Timing.RunCoroutine(PlayerEffectShower());
            }
            else
            {
                this.IsEnabled = false;
                Timing.KillCoroutines(this.Current);
                return;
            }
        }

        private IEnumerator<float> PlayerEffectShower()
        {
            Configs cfg = Plugin.Instance.Config;
            for (; ; )
            {
                if (player == null)
                {
                    Log.Debug($"{nameof(PlayerEffectShower)}: Player is null break yield");
                    yield break;
                }
                if (IsEnabled == false)
                {
                    Log.Debug($"{nameof(PlayerEffectShower)}: IsEnabled - {IsEnabled} disabled, break yield");
                    yield break;
                }
                if (player.IsDead | Plugin.Instance.Config.IgnoredRoles.Contains(player.Role.Type))
                {
                    yield return Timing.WaitForSeconds(2);
                    continue;
                }
                if (player.ActiveEffects.Where(x => !Plugin.Instance.Config.BlackList.Contains(x.GetEffectType())).Count() == 0)
                {
                    yield return Timing.WaitForSeconds(0.1f);
                    continue;
                }
                StringBuilder InfoLine = new StringBuilder();
                foreach (var item in player.ActiveEffects)
                {
                    try
                    {
                        if (Plugin.Instance.Config.BlackList.Contains(item.GetEffectType())) continue;
                        try
                        {
                            string processingline = cfg.EffectLine[item.Classification];
                            if (string.IsNullOrWhiteSpace(processingline)) continue;
                            processingline = processingline.Replace("%time%", (int)item.Duration == 0 ? "inf" : ((int)item.TimeLeft).ToString());
                            processingline = processingline.Replace("%duration%", (int)item.Duration == 0 ? "inf" : item.Duration.ToString());
                            processingline = processingline.Replace("%effect%", cfg.GetName(item.GetEffectType()));
                            processingline = processingline.Replace("%intensity%", item.Intensity.ToString());
                            Log.Debug($"{nameof(PlayerEffectShower)}: Line {processingline} created");
                            InfoLine.AppendLine(processingline);
                        }
                        catch (Exception e)
                        {
                            Log.Debug($"{nameof(PlayerEffectShower)}: Exception {e.Message}");
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"{nameof(PlayerEffectShower)}: Exception {e.Message}");
                    }
                }
                string data = InfoLine.ToString();
                Log.Debug(data);
                try
                {
                    data = $"<size={cfg.NativeHintSettings.FontSize}><align={cfg.NativeHintSettings.Aligment}>" + data + "</size></align>";
                    player.ShowHint(data, 1f + (float)(player.Ping / 100f)); // display a message taking into account the player's ping for a smooth update
                }
                catch (Exception e)
                {
                    Log.Debug($"{nameof(PlayerEffectShower)}: Exception {e.Message}");
                }
                Log.Debug($"{nameof(PlayerEffectShower)}: Iteration {player.Nickname}: processed.");
                yield return Timing.WaitForSeconds(cfg.UpdateTime);
            }
        }
    }
}