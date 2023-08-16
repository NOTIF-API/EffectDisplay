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
        private Config _Config;

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
            EffectCategory category = effectType.GetCategories();
            if (category == EffectCategory.Positive | category == EffectCategory.Movement)
            {
                return _Config.GoodTypeWriting;
            }
            if (effectType == EffectType.Scp207)
            {
                return _Config.MixedTypeWriting;
            }
            else
            {
                return _Config.BadTypeWriting;
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
                StringBuilder ShowningText = new StringBuilder();
                ShowningText.AppendLine("\n\n\n");
                foreach (StatusEffectBase effect in  _Player.ActiveEffects)
                {
                    if (effect.IsEnabled)
                    {
                        string EffectLine = _Config.EffectMessage;
                        EffectLine = EffectLine.Replace("{type}", IEffectCategory(effect.GetEffectType()));
                        EffectLine = EffectLine.Replace("{duration}", Convert.ToInt32(effect.TimeLeft).ToString());
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
            Destroy(this);
        }
    }
}
