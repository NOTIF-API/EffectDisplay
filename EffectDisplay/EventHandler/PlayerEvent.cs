using EffectDisplay.Components;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace EffectDisplay.EventHandler
{
    public class PlayerEvent
    {
        public void OnVerefied(VerifiedEventArgs e)
        {
            if (e.Player == null | e.Player.GameObject.GetComponent<UserEffectDisplayer>() != null)
            {
                return;
            }
            else
            {
                e.Player.GameObject.AddComponent<UserEffectDisplayer>();
            }
        }
    }
}
