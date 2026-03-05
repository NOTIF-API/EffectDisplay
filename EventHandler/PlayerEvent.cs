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
        public static void OnVerefied(VerifiedEventArgs e)
        {
            if (e.Player == null) return;
            Log.Debug(e.Player);
            e.Player.GameObject.AddComponent<UserEffectDisplayer>();
            Log.Debug($"{nameof(OnVerefied)}: Added {nameof(UserEffectDisplayer)} components.");
        }
    }
}