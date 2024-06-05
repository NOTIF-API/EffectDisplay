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

        public override Version Version { get; } = new Version(2, 0, 0);

        public override Version RequiredExiledVersion { get; } = new Version(8, 9, 0);

        public static Plugin Instance { get; private set; }

        public static DataBase data { get; private set; }

        private PlayerEvent pl { get; set; } = null;

        public override void OnEnabled()
        {
            CheckDataBase();
            Instance = this;
            Timing.CallDelayed(0.5f, () => {
                data = new DataBase();
            });
            pl = new PlayerEvent();
            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
            data.Dispose();
            data = null;
            Instance = null;
            pl = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.Verified += pl.OnVerefied;
        }
        private void UnRegisterEvents()
        {
            Exiled.Events.Handlers.Player.Verified -= pl.OnVerefied;
        }
        /// <summary>
        /// checks for the presence of a database folder with configurations provided
        /// </summary>
        private void CheckDataBase()
        {
            Log.Debug("Checking existing data base");
            if (!this.Config.IsDatabaseUse)
            {
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
