using Exiled.Events.EventArgs.Player;
using EffectDisplay.Extension;

namespace EffectDisplay.EventHandler
{
    public class RoundEvent
    {
        public void OnVerefied(VerifiedEventArgs e)
        {
            e.Player.GameObject.AddComponent<EffectReader>();
        }
    }
}
