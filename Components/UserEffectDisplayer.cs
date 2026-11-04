using EffectDisplay.Features;
using EffectDisplay.Features.Sereliazer;

using Exiled.API.Extensions;
using Exiled.API.Features;

using MEC;

using NorthwoodLib.Pools;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EffectDisplay.Components
{
    public class UserEffectDisplayer : MonoBehaviour
    {
        private Player player;
        /// <summary>
        /// Stores the current process for future stopping
        /// </summary>
        private CoroutineHandle Current;

        private bool Enabled = true;
        /// <summary>
        /// Determines whether the component is enabled or not. If set to true, the component will start showing the effects of the player. If set to false, the component will stop showing the effects of the player.
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
                if (value) Current = Timing.RunCoroutine(PlayerEffectShower());
            }
        }

        private void Awake()
        {
            Log.Debug($"[Awake] Initing component.");
            player = Player.Get(gameObject);
            Log.Debug($"[Awake] {player.Nickname} Awake and adding component handling.");
            if (!DataBase.TryGetData(player.UserId, out User user))
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = user.IsAllow;
            }
        }

        private string GenerateString(CustomPlayerEffects.StatusEffectBase effect)
        {
            string processingline = Plugin.Instance.Config.EffectLine[effect.Classification];
            if (string.IsNullOrWhiteSpace(processingline)) return string.Empty;
            processingline = processingline
                .Replace("%time%", (int)effect.Duration == 0 ? "inf" : $"{effect.TimeLeft:F1}")
                .Replace("%duration%", effect.Duration <= 0.1f ? "inf" : $"{effect.Duration:F0}")
                .Replace("%effect%", Plugin.Instance.Config.GetName(effect.GetEffectType()))
                .Replace("%intensity%", effect.Intensity.ToString());
            Log.Debug($"[GenerateString]: Line {processingline} created");
            return processingline;
        }

        private IEnumerator<float> PlayerEffectShower()
        {
            for (; ; )
            {
                if (player == null || IsEnabled == false)
                {
                    Log.Debug($"[PlayerEffectShower] Player is null or component is disabled, break yield");
                    yield break;
                }
                if (player.IsDead || Plugin.Instance.Config.IgnoredRoles.Contains(player.Role.Type))
                {
                    yield return Timing.WaitForSeconds(2);
                    continue;
                }

                if (!player.ActiveEffects.Any(x => !Plugin.Instance.Config.BlackList.Contains(x.GetEffectType())))
                {
                    yield return Timing.WaitForSeconds(2f);
                    continue;
                }

                StringBuilder InfoLine = StringBuilderPool.Shared.Rent();
                InfoLine.Append($"<size={Plugin.Instance.Config.NativeHintSettings.FontSize}><align={Plugin.Instance.Config.NativeHintSettings.Aligment}>");
                
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
                        Log.Debug($"[PlayerEffectShower] Exception {e}");
                    }
                }

                InfoLine.Append("</align></size>");
                player.ShowHint(StringBuilderPool.Shared.ToStringReturn(InfoLine), Plugin.Instance.Config.UpdateTime);

                Log.Debug($"[PlayerEffectShower] Iteration {player.Nickname}: processed.");
                yield return Timing.WaitForSeconds(Plugin.Instance.Config.UpdateTime);
            }
        }
    }
}