using EffectDisplay.Components;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;

using JetBrains.Annotations;

using MEC;

namespace EffectDisplay.EventHandler
{
    public class PlayerEvent
    {
        public static HeaderSetting Header { get; private set; }
        public static TwoButtonsSetting UpdateEnabler { get; private set; }
        
        public PlayerEvent()
        {
            Configs cfg = Plugin.Instance.Config;
            Header = new HeaderSetting
                (
                cfg.HeaderId,
                "Effect display setting",
                cfg.HeaderDescription
                );
            UpdateEnabler = new TwoButtonsSetting(
            cfg.TwoButtonId,
            cfg.TwoButtonLabel,
            cfg.TwoButtonEnabled,
            cfg.TwoButtonDisabled,
            header: Header,
            hintDescription: cfg.EnabledDisplayDescription,
            onChanged: OnUpdateSetting
            );
            SettingBase.Register(new[] { UpdateEnabler });
        }

        public void OnVerefied(VerifiedEventArgs e)
        {
            if (e.Player == null) return;
            Log.Debug(e.Player);
            e.Player.GameObject.AddComponent<UserEffectDisplayer>();
            Log.Debug($"{nameof(OnVerefied)}: Added {nameof(UserEffectDisplayer)} components.");
        }

        private void OnUpdateSetting(Player player, SettingBase twr)
        {
            if (player == null || twr == null) return;
            if (twr is TwoButtonsSetting tbs)
            {
                bool chose = tbs.IsFirst;
                Plugin.data.IsAllow(player.UserId, chose);
                player.GameObject.GetComponent<UserEffectDisplayer>().IsEnabled = chose;
            }
        }
    }
}
