using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Enums;
using System.Text;
using System.Collections.Generic;
using MEC;
using CustomPlayerEffects;
using Exiled.API.Extensions;
using System;
using System.Linq;

namespace EffectDisplay.Extension
{
    public class EffectReader: MonoBehaviour
    {
        public static int TotalEffectReaderComponents { get; private set; } = 0;

        private CoroutineHandle _CoroutineHandle;

        private Player _Player;

        private string _Name;

        private const float EndlessEffectTime = 0.1f;
        /// <summary>
        /// determines whether the player wants to not see the message about active effects
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        private void Awake()
        {
            _Player = Player.Get(transform.gameObject);
            _Name = _Player.Nickname;
            if (_Player == null )
            {
                Log.Debug("GameObject not contain player classification");
                Destroy(this);
            }
            Log.Debug($"Added component to {_Player.Nickname}");
            _CoroutineHandle = Timing.RunCoroutine(EffectListener(), $"Efl-{_Player.Id}");
            TotalEffectReaderComponents += 1;
        }
        private string IEffectCategory(EffectType effectType)
        {
            if (effectType == EffectType.Scp207)
            {
                return Main.Instance.Config.MixedTypeWriting;
            }

            if (effectType.IsHarmful() || effectType.IsNegative())
            {
                return Main.Instance.Config.BadTypeWriting;
            }
            else
            {
                return Main.Instance.Config.GoodTypeWriting;
            }
        }
        private void OnDestroy()
        {
            Timing.KillCoroutines(this._CoroutineHandle);
            Log.Debug($"Dissconecting and destroying component. {_Name}");
            TotalEffectReaderComponents -= 1;
        }
        private IEnumerator<float> EffectListener()
        {
            for (; ; )
            {
                if (this._Player == null)
                {
                    break;
                }
                if (this.IsEnabled & !this._Player.IsDead)
                {
                    StringBuilder ShowningText = new StringBuilder();
                    ShowningText.AppendLine("\n\n\n");
                    foreach (StatusEffectBase effect in this._Player.ActiveEffects.Where(x => x.IsEnabled))
                    {
                        if (Main.Instance.Config.BlackListEffect.Contains(effect.GetEffectType()))
                        {
                            continue;
                        }
                        else
                        {
                            string EffectLine = Main.Instance.Config.EffectMessage;
                            EffectLine = EffectLine.Replace("{type}", IEffectCategory(effect.GetEffectType()));
                            if (effect.Duration > EndlessEffectTime)
                            {
                                EffectLine = EffectLine.Replace("{duration}", Convert.ToInt32(effect.TimeLeft).ToString());
                            }
                            else
                            {
                                EffectLine = EffectLine.Replace("{duration}", "inf");
                            }
                            EffectLine = EffectLine.Replace("{effect}", effect.GetEffectType().ToString());
                            EffectLine = EffectLine.Replace("{intensivity}", effect.Intensity.ToString());
                            ShowningText.AppendLine(EffectLine);

                        }
                    }
                    _Player.ShowHint($"{ShowningText.ToString()}", 1);
                }
                yield return Timing.WaitForSeconds(Main.Instance.Config.TextUpdateTime);
            }
            Destroy(this);
        }
    }
}