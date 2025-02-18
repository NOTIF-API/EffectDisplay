using EffectDisplay.Extensions;
using EffectDisplay.Features;

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
        private MeowHintManager meow;
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

        private void ShowMeowHint(string text)
        {
            if (meow != null)
            {
                MeowHintManager.RemoveHint(player, meow.GetHintProcessionObject());
                meow = null;
            }
            meow = new MeowHintManager(text, "EffectDisplay", Plugin.Instance.Config.MeowHintSettings);
            MeowHintManager.AddHint(player, meow.GetHintProcessionObject());
        }

        private IEnumerator<float> PlayerEffectShower()
        {
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
                    if (meow != null)
                    {
                        meow = null;
                    }
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
                            string processingline = Plugin.Instance.Config.EffectLine[item.Classification];
                            if (string.IsNullOrEmpty(processingline)) continue;
                            processingline = processingline.Replace("%time%", item.Duration == 0 ? "inf" : ((int)item.TimeLeft).ToString());
                            processingline = processingline.Replace("%duration%", item.Duration == 0 ? "inf" : item.Duration.ToString());
                            processingline = processingline.Replace("%effect%", Plugin.Instance.Config.GetName(item.GetEffectType()));
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
                    if (Plugin.HintServiceMeowDetected)
                    {
                        ShowMeowHint(data);
                    }
                    else
                    {
                        data = $"<size={Plugin.Instance.Config.NativeHintSettings.FontSize}><align={Plugin.Instance.Config.NativeHintSettings.Aligment}>" + data + "</size></align>";
                        player.ShowHint(data, 1 + (player.Ping / 100)); // display a message taking into account the player's ping for a smooth update
                    }
                }
                catch (Exception e)
                {
                    Log.Debug($"{nameof(PlayerEffectShower)}: Exception {e.Message}");
                }
                Log.Debug($"{nameof(PlayerEffectShower)}: Iteration {player.Nickname}: processed.");
                yield return Timing.WaitForSeconds(Plugin.Instance.Config.UpdateTime);
            }
        }
    }
}