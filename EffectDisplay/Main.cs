using Exiled.API.Features;
using EffectDisplay.EventHandler;
using player = Exiled.Events.Handlers.Player;

namespace EffectDisplay
{
    public class Main: Plugin<Config>
    {
        public static Main Instance { get; private set; }
        private RoundEvent RoundEvent { get; set; } = null;

        public override void OnEnabled()
        {
            Instance = this;
            RoundEvent = new RoundEvent();
            player.Verified += RoundEvent.OnVerefied;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;
            player.Verified -= RoundEvent.OnVerefied;
            RoundEvent = null;
            base.OnDisabled();
        }
    }
}
