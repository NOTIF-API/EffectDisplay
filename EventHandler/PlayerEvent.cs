﻿using EffectDisplay.Components;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;

using MEC;

namespace EffectDisplay.EventHandler
{
    public class PlayerEvent
    {
        public static TwoButtonsSetting UpdateEnabler { get; private set; }
        
        public PlayerEvent()
        {
            UpdateEnabler = new TwoButtonsSetting(
            1,
            "Effect display",
            "ON",
            "OFF",
            hintDescription: Plugin.Instance.Config.EnabledDisplayDescription,
            onChanged: OnUpdateSetting
            );
            SettingBase.Register(new[] { UpdateEnabler });
        }

        public void OnVerefied(VerifiedEventArgs e)
        {
            Log.Debug(e.Player);
            e.Player?.GameObject.AddComponent<UserEffectDisplayer>();
            Log.Debug($"{nameof(OnVerefied)}: Added {nameof(UserEffectDisplayer)} components.");
        }

        private void OnUpdateSetting(Player player, SettingBase twr)
        {
            if (twr.Id != UpdateEnabler.Id) return;
            else
            {
                bool chose = (twr as TwoButtonsSetting).IsFirst;
                Plugin.data.IsAllow(player.UserId, chose);
                player.GameObject.GetComponent<UserEffectDisplayer>().IsEnabled = chose;
            }
        }
    }
}
