using EffectDisplay.Components;
using Exiled.Events.EventArgs.Player;

namespace EffectDisplay.EventHandler
{
    public class PlayerEvent
    {
        public void OnVerefied(VerifiedEventArgs e)
        {
            e.Player?.GameObject.AddComponent<UserEffectDisplayer>();
        }
    }
}
