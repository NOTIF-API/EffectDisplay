using CustomPlayerEffects;
using EffectDisplay.Extensions;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EffectDisplay.Components
{
    public class UserEffectDisplayer: MonoBehaviour
    {
        private Player player;

        private string Category(EffectType effectType)
        {
            if (effectType == EffectType.Scp207 | effectType == EffectType.AntiScp207)
            {
                return Plugin.Instance.Config.MixedEffect;
            }

            if (effectType.IsHarmful() || effectType.IsNegative())
            {
                return Plugin.Instance.Config.BadEffect;
            }
            else
            {
                return Plugin.Instance.Config.GoodEffect;
            }
        }

        private void Awake()
        {
            player = Player.Get(gameObject);
            if (!player.DataChoise())
            {
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
                StringBuilder output = new StringBuilder();
                output.Append("\n\n\n\n");
                if (ply == null || !ply.IsConnected | !ply.DataChoise())
                {
                    Destroy(this);
                    break;
                }
                else
                {
                    foreach (StatusEffectBase type in ply.ActiveEffects)
                    {
                        EffectType effect = type.GetEffectType();
                        string name = "";
                        if (Plugin.Instance.Config.EffectTranslation.ContainsKey(effect))
                        {
                            name = Plugin.Instance.Config.EffectTranslation[effect];
                        }
                        if (!Plugin.Instance.Config.EffectTranslation.ContainsKey(effect))
                        {
                            name = effect.ToString();
                        }
                        string line = $"{Plugin.Instance.Config.HintLocation}{Plugin.Instance.Config.EffectLineMessage.Replace("%effect%", name)}</align>";
                        if (type.Duration < 0.1)
                        {
                            line = line.Replace("%time%", "inf");
                        }
                        if (type.Duration > 0.1)
                        {
                            line = line.Replace("%time%", ((int)type.TimeLeft).ToString());
                        }
                        line = line.Replace("%type%", Category(effect));
                        output.AppendLine(line);
                    }
                    Log.Debug($"Try to show hint for ply {ply.Nickname}");
                    ply.ShowHint(output.ToString(), 1);
                }
                yield return Timing.WaitForSeconds(0.9f);
            }
        }
    }
}
