using EffectDisplay.EventHandler;
using EffectDisplay.Features;
using Exiled.API.Features;
using MEC;
using System;
using System.IO;

namespace EffectDisplay
{
    public class Plugin : Plugin<Configs>
    {
        public override string Author { get; } = "notif";

        public override string Name { get; } = "EffectDisplay";

        public override Version Version { get; } = new Version(2, 0, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public static Plugin Instance { get; private set; }

        public static DataBase data { get; private set; }

        private PlayerEvent Event { get; set; }

        public override void OnEnabled()
        {
            CheckDataBase();
            Instance = this;
            Timing.CallDelayed(0.5f, () => {
                data = new DataBase();
            });
            Event = new PlayerEvent();
            SubscribeEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnsubscribeEvents();
            data.Dispose();
            data = null;
            Instance = null;
            Event = null;
            base.OnDisabled();
        }

        protected void SubscribeEvents()
        {
            Log.Debug($"{nameof(SubscribeEvents)} starting registering event");
            Exiled.Events.Handlers.Player.Verified += Event.OnVerefied;
        }

        protected void UnsubscribeEvents()
        {
            Log.Debug($"{nameof(UnsubscribeEvents)} starting unregistering event");
            Exiled.Events.Handlers.Player.Verified -= Event.OnVerefied;
        }
        /// <summary>
        /// checks for the presence of a database folder with configurations provided
        /// </summary>
        private void CheckDataBase()
        {
            Log.Debug($"{nameof(CheckDataBase)} Checking existing data base");
            if (!this.Config.IsDatabaseUse)
            {
                Log.Warn("Database usage is disabled, players will not be able to enable effects display");
                return;
            }
            else
            {
                string file_path = Path.Combine(this.Config.PathToDataBase, this.Config.DatabaseName);
                // if folder do not detected creating it with file
                if (!Directory.Exists(this.Config.PathToDataBase))
                {
                    Directory.CreateDirectory(this.Config.PathToDataBase);
                    File.Create(file_path);
                    Log.Warn("do not founded folder with data base creating...");
                    return;
                }
                if (!File.Exists(file_path))
                {
                    Log.Warn("do not founded db file");
                    File.Create(file_path);
                }
            }
        }
    }
}
