using EffectDisplay.Components;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace EffectDisplay.EventHandler
{
    public class PlayerEvent
    {
        public static void OnVerefied(VerifiedEventArgs e)
        {
            if (e.Player == null) return;
            Log.Debug($"[OnVerefied] Player verified: {e.Player.Nickname}");
            e.Player.GameObject.AddComponent<UserEffectDisplayer>();
            Log.Debug($"[OnVerefied] Added UserEffectDisplayer component to {e.Player.Nickname}");
        }
    }
}