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
        private StringBuilder sb;
        private const float EndlessEffectTime = 0.1f;
        private List<EffectType> Positive { get; set; } = new List<EffectType>()
        { 
            EffectType.Invigorated,
            EffectType.Invisible,
            EffectType.RainbowTaste,
            EffectType.BodyshotReduction,
            EffectType.DamageReduction,
            EffectType.MovementBoost,
            EffectType.Vitality,
            EffectType.Scp1853,
            EffectType.SpawnProtected
        };
        private List<EffectType> Negative { get; set; } = new List<EffectType>()
        {
            EffectType.AmnesiaVision,
            EffectType.AmnesiaItems,
            EffectType.Asphyxiated,
            EffectType.Bleeding,
            EffectType.Blinded,
            EffectType.Burned,
            EffectType.Concussed,
            EffectType.Corroding,
            EffectType.PocketCorroding,
            EffectType.Deafened,
            EffectType.Decontaminating,
            EffectType.Disabled,
            EffectType.Ensnared,
            EffectType.Exhausted,
            EffectType.Flashed,
            EffectType.Hemorrhage,
            EffectType.Hypothermia,
            EffectType.Poisoned,
            EffectType.SinkHole,
            EffectType.Stained,
            EffectType.SeveredHands,
            EffectType.Traumatized,
            EffectType.CardiacArrest,
            EffectType.Scanned
        };
        private Config _Config;
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
            _Config = Main.Instance.Config;
            TotalEffectReaderComponents += 1;
        }
        private string IEffectCategory(EffectType effectType)
        {
            if (Positive.Contains(effectType))
            {
                return _Config.GoodTypeWriting;
            }
            if (Negative.Contains(effectType))
            {
                return _Config.BadTypeWriting;
            }
            else
            {
                return _Config.MixedTypeWriting;
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
                        if (_Config.BlackListEffect.Contains(effect.GetEffectType()))
                        {
                            continue;
                        }
                        else
                        {
                            string EffectLine = this._Config.EffectMessage;
                            EffectLine = EffectLine.Replace("{type}", IEffectCategory(effect.GetEffectType()));
                            if (effect.Duration >= EndlessEffectTime)
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
                yield return Timing.WaitForSeconds(this._Config.TextUpdateTime);
            }
            Destroy(this);
        }
    }
}
