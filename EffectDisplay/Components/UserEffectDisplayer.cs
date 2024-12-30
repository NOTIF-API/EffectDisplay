using EffectDisplay.Extensions;
using EffectDisplay.Features;

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
        private MeowHintManager MeowHintManager;
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
            if (Plugin.HintServiceMeowDetected)
            {
                MeowHintManager = new MeowHintManager();
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
            bool showing = false;
            for (; ; )
            {
                if (player == null)
                {
                    yield break;
                }
                if (IsEnabled == false)
                {
                    yield break;
                }
                if (player.IsDead | Plugin.Instance.Config.IgnoredRoles.Contains(player.Role.Type))
                {
                    yield return Timing.WaitForSeconds(2);
                    continue;
                }
                if (player.ActiveEffects.Where(x => !Plugin.Instance.Config.BlackList.Contains(x.GetEffectType())).Count() == 0)
                {
                    MeowHintManager.RemoveHint(player, MeowHintManager.GetHintProcessionObject());
                    showing = false;
                    yield return Timing.WaitForSeconds(0.1f);
                    continue;
                }
                if (Plugin.HintServiceMeowDetected & showing)
                {
                    MeowHintManager.RemoveHint(player, MeowHintManager.GetHintProcessionObject());
                    showing = false;
                }
                StringBuilder InfoLine = new StringBuilder();
                if (!Plugin.HintServiceMeowDetected)
                {
                    InfoLine.Append("\n\n\n\n");
                    InfoLine.AppendLine($"<size={Plugin.Instance.Config.NativeHintSettings.FontSize}><align={Plugin.Instance.Config.NativeHintSettings.Aligment.ToLower()}>");
                }
                foreach (var item in player.ActiveEffects)
                {
                    if (Plugin.Instance.Config.BlackList.Contains(item.GetEffectType())) continue;
                    string processingline = Plugin.Instance.Config.EffectLine[item.Classification];
                    if (string.IsNullOrEmpty(processingline)) continue;
                    processingline = processingline.Replace("%time%", ((int)item.TimeLeft) == 0 ? string.Empty : ((int)item.TimeLeft).ToString());
                    processingline = processingline.Replace("%duration%", item.Duration == 0 ? "inf" : ((int)item.Duration).ToString());
                    processingline = processingline.Replace("%effect%", Plugin.Instance.Config.GetName(item.GetEffectType()));
                    processingline = processingline.Replace("%intensity%", item.Intensity.ToString());
                    InfoLine.AppendLine(processingline);
                }
                if (Plugin.HintServiceMeowDetected)
                {
                    MeowHintManager = new MeowHintManager();
                    MeowHintManager.SetText(InfoLine.ToString());
                    MeowHintManager.SetId("EffectDisplay");
                    MeowHintManager.SetFont(Plugin.Instance.Config.MeowHintSettings.FontSize);
                    MeowHintManager.SetHorizontalAligment(Plugin.Instance.Config.MeowHintSettings.Aligment);
                    MeowHintManager.SetVerticalAligment(Plugin.Instance.Config.MeowHintSettings.VerticalAligment);
                    MeowHintManager.SetYCoordinates(Plugin.Instance.Config.MeowHintSettings.YCoordinate);
                    MeowHintManager.SetXCoordinate(Plugin.Instance.Config.MeowHintSettings.XCoordinate);
                    MeowHintManager.AddHint(player, MeowHintManager.GetHintProcessionObject());
                    showing = true;
                }
                else if (!Plugin.HintServiceMeowDetected)
                {
                    string res = InfoLine.ToString() + "</size></align>";
                    player.ShowHint(res, (float)Plugin.Instance.Config.UpdateTime);
                }
                Log.Debug($"Iteration {player.Nickname}: processed.");
                yield return Timing.WaitForSeconds((float)Plugin.Instance.Config.UpdateTime);
            }
        }
    }
}