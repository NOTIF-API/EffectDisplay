using EffectDisplay.Components;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace EffectDisplay.EventHandler
{
    public class PlayerEvent
    {
        public void OnVerefied(VerifiedEventArgs e)
        {
            Log.Debug(e.Player);
            e.Player?.GameObject.AddComponent<UserEffectDisplayer>();
            Log.Debug($"{nameof(OnVerefied)} Added {nameof(UserEffectDisplayer)} components");
        }
    }
}
