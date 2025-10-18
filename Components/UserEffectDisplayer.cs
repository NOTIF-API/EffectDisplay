using EffectDisplay.Extensions;

using Exiled.API.Extensions;
using Exiled.API.Features;

using MEC;

using System;
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
                Log.Debug($"{nameof(IsEnabled)}.Set: {Enabled} -> {value}.");
                Enabled = value;
                if (value == false) Timing.KillCoroutines(Current);
                else Current = Timing.RunCoroutine(PlayerEffectShower());
            }
        }

        public Configs cfg 
        { 
            get
            {
                return Plugin.Instance.Config ?? new Configs();
            }
        }

        private void Awake()
        {
            Log.Debug($"{nameof(Awake)}: Initing component.");
            player = Player.Get(gameObject);
            Log.Debug($"{nameof(Awake)}: {player.Nickname} Awake and adding component handling.");
            if (player.GetIsAllow()) Current = Timing.RunCoroutine(PlayerEffectShower());
            else Destroy(this);
        }

        private string GenerateString(CustomPlayerEffects.StatusEffectBase effect)
        {
            string processingline = cfg.EffectLine[effect.Classification];
            if (string.IsNullOrWhiteSpace(processingline)) return string.Empty;
            processingline = processingline.Replace("%time%", (int)effect.Duration == 0 ? "inf" : ((int)effect.TimeLeft).ToString());
            processingline = processingline.Replace("%duration%", (int)effect.Duration == 0 ? "inf" : effect.Duration.ToString());
            processingline = processingline.Replace("%effect%", cfg.GetName(effect.GetEffectType()));
            processingline = processingline.Replace("%intensity%", effect.Intensity.ToString());
            Log.Debug($"{nameof(PlayerEffectShower)}: Line {processingline} created");
            return processingline;
        }

        private IEnumerator<float> PlayerEffectShower()
        {
            for (; ; )
            {
                if (player == null || IsEnabled == false)
                {
                    Log.Debug($"{nameof(PlayerEffectShower)}: Player is null or component is disabled, break yield");
                    yield break;
                }
                if (player.IsDead || Plugin.Instance.Config.IgnoredRoles.Contains(player.Role.Type))
                {
                    yield return Timing.WaitForSeconds(2);
                    continue;
                }
                if (player.ActiveEffects.Where(x => !Plugin.Instance.Config.BlackList.Contains(x.GetEffectType())).Count() == 0)
                {
                    yield return Timing.WaitForSeconds(2f);
                    continue;
                }

                StringBuilder InfoLine = new StringBuilder();
                foreach (CustomPlayerEffects.StatusEffectBase item in player.ActiveEffects)
                {
                    if (Plugin.Instance.Config.BlackList.Contains(item.GetEffectType())) continue;
                    try
                    {
                        string line = GenerateString(item);
                        if (string.IsNullOrEmpty(line)) continue;
                        InfoLine.AppendLine(line);
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"{nameof(PlayerEffectShower)}: Exception {e.Message}");
                    }
                }

                string data = InfoLine.ToString();
                Log.Debug(data);

                data = $"<size={cfg.NativeHintSettings.FontSize}><align={cfg.NativeHintSettings.Aligment}>" + data + "</size></align>";
                player?.ShowHint(data, 1f + (float)(player.Ping / 100f)); // display a message taking into account the player's ping for a smooth update
                
                Log.Debug($"{nameof(PlayerEffectShower)}: Iteration {player.Nickname}: processed.");
                yield return Timing.WaitForSeconds(cfg.UpdateTime);
            }
        }
    }
}