using EffectDisplay.Components;
using EffectDisplay.Features.Sereliazer;

using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;

namespace EffectDisplay.Features
{
    public class ServerSpecifiqManager
    {
        public static HeaderSetting Header { get; private set; } = null;
        public static TwoButtonsSetting UpdateEnabler { get; private set; } = null;

        public static void Init(Configs cfg)
        {
            if (Header != null | UpdateEnabler != null)
            {
                SettingBase.Unregister((p) => { return true; }, new SettingBase[] { Header, UpdateEnabler });
            }
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

        public static void Disable()
        {
            SettingBase.Unregister((p) => { return true; }, new SettingBase[] { Header, UpdateEnabler });
            Header = null;
            UpdateEnabler = null;
        }

        private static void OnUpdateSetting(Player player, SettingBase twr)
        {
            if (player == null || twr == null) return;
            if (twr is TwoButtonsSetting tbs)
            {
                bool chose = tbs.IsFirst;
                User user = null;
                if (!DataBase.TryGetData(player.UserId, out user))
                {
                    user = new User()
                    {
                        UserId = player.UserId,
                        IsAllow = chose
                    };
                }
                DataBase.TryUpdate(user);
                if (player.GameObject.TryGetComponent<UserEffectDisplayer>(out UserEffectDisplayer component))
                {
                    component.IsEnabled = chose;
                }
            }
        }
    }
}
