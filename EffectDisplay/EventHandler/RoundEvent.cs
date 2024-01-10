using Exiled.Events.EventArgs.Player;
using EffectDisplay.Extension;
using MEC;

namespace EffectDisplay.EventHandler
{
    public class RoundEvent
    {
        public void OnVerefied(VerifiedEventArgs e)
        {
            if (e.Player == null) return;
            if (!Main.Instance.DataBaseManager.GetMemberChose(e.Player.UserId))
            {
                Timing.CallDelayed(0.2f, () => 
                {
                    e.Player.GameObject.AddComponent<EffectReader>();
                });
            }
            else
            {
                Timing.CallDelayed(0.2f, () =>
                {
                    e.Player.GameObject.AddComponent<EffectReader>().IsEnabled = false;
                });
                return;
            }
        }
    }
}
