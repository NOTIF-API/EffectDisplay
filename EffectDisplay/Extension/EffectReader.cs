using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Enums;
using System.Text;
using System.Collections.Generic;
using MEC;
using CustomPlayerEffects;
using Exiled.API.Extensions;
using System;

namespace EffectDisplay.Extension
{
    public class EffectReader: MonoBehaviour
    {
        private CoroutineHandle _CoroutineHandle;
        private Player _Player;
        private StringBuilder sb;
        private List<EffectType> Good { get; set; } = new List<EffectType>()
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
        private List<EffectType> Bad { get; set; } = new List<EffectType>()
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
        public bool IsDisabled { get; private set; } = false;
        /// <summary>
        /// updates the value of IsDisabled
        /// </summary>
        /// <param name="value">passed value</param>
        public void StatUpdate(bool value)
        {
            if (IsDisabled == value)
            {
                return;
            }
            else
            {
                IsDisabled = value;
            }
        }

        private void Awake()
        {
            _Player = Player.Get(transform.gameObject);
            if (_Player == null )
            {
                Log.Debug("GameObject not contain player classification");
                Destroy(this);
            }
            Log.Debug($"Added component to {_Player.Nickname}");
            _CoroutineHandle = Timing.RunCoroutine(EffectListener(), $"Efl-{_Player.Id}");
            _Config = Main.Instance.Config;
        }
        private string IEffectCategory(EffectType effectType)
        {
            if (Good.Contains(effectType))
            {
                return _Config.GoodTypeWriting;
            }
            if (Bad.Contains(effectType))
            {
                return _Config.BadTypeWriting;
            }
            else
            {
                return _Config.MixedTypeWriting;
            }
        }
        
        private IEnumerator<float> EffectListener()
        {
            for (; ; )
            {
                if (_Player == null)
                {
                    break;
                }
                if (!IsDisabled)
                {
                    StringBuilder ShowningText = new StringBuilder();
                    ShowningText.AppendLine("\n\n\n");
                    foreach (StatusEffectBase effect in _Player.ActiveEffects)
                    {
                        if (effect.IsEnabled)
                        {
                            string EffectLine = _Config.EffectMessage;
                            EffectLine = EffectLine.Replace("{type}", IEffectCategory(effect.GetEffectType()));
                            if (effect.Duration >= 0.2f)
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
                        else
                        {
                            continue;
                        }
                    }
                    _Player.ShowHint($"{ShowningText.ToString()}", 1);
                    yield return Timing.WaitForSeconds(0.9f);
                }
            }
            Destroy(this);
        }
    }
}
