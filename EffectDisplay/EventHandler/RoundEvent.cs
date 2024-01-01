using Exiled.Events.EventArgs.Player;
using EffectDisplay.Extension;

namespace EffectDisplay.EventHandler
{
    public class RoundEvent
    {
        public void OnVerefied(VerifiedEventArgs e)
        {
            if (!Main.Instance.DataBaseManager.GetMemberChose(e.Player.UserId))
            {
                e.Player.GameObject.AddComponent<EffectReader>();
            }
            else
            {
                e.Player.GameObject.AddComponent<EffectReader>().IsEnabled = false;
                return;
            }
        }
    }
}
