using EffectDisplay.EventHandler;
using EffectDisplay.Features;
using Exiled.API.Features;
using Exiled.Loader;

using MEC;
using System;
using System.IO;
using System.Linq;

using UnityEngine;

namespace EffectDisplay
{
    public class Plugin : Plugin<Configs>
    {
        public override string Author { get; } = "notif";

        public override string Name { get; } = "EffectDisplay";

        public override Version Version { get; } = new Version(2, 2, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public static Plugin Instance { get; private set; }

        public static DataBase data { get; private set; }

        private PlayerEvent Event { get; set; }

        public static bool HintServiceMeowDetected { get; private set; } = false;

        public override void OnEnabled()
        {
            DBCondition();
            Instance = this;
            // if not delayed exception called
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
            HintServiceMeowDetected = false;
            base.OnDisabled();
        }

        protected void SubscribeEvents()
        {
            Log.Debug($"{nameof(SubscribeEvents)}: Subscribe to the event.");
            Exiled.Events.Handlers.Player.Verified += Event.OnVerefied;
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayers;
        }
        /// <summary>
        /// needed to check the presence of third-party plugins after they are fully loaded
        /// </summary>
        private void OnWaitingForPlayers()
        {
            if (Config.ThirdParty)
            {
                if (Loader.Plugins.Where(x => x.Name == "HintServiceMeow").FirstOrDefault() != null)
                {
                    HintServiceMeowDetected = true;
                    Log.Info($"{nameof(OnWaitingForPlayers)}: A third-party provider has been detected. The Hint plugin will be adjusted to work with it automatically.");
                }
            }
        }

        protected void UnsubscribeEvents()
        {
            Log.Debug($"{nameof(UnsubscribeEvents)}: Unsubscribe from events.");
            Exiled.Events.Handlers.Player.Verified -= Event.OnVerefied;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWaitingForPlayers;
        }
        /// <summary>
        /// Checks whether the conditions for working with the database are satisfied and restores missing files if necessary
        /// </summary>
        private void DBCondition()
        {
            if (!this.Config.IsDatabaseUse)
            {
                Log.Warn($"{nameof(DBCondition)}: DB usage is disabled by configuration.");
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
                    Log.Warn($"{nameof(DBCondition)}: The directory with the database file is missing.");
                    return;
                }
                if (!File.Exists(file_path))
                {
                    Log.Warn($"{nameof(DBCondition)}: DB file is missing.");
                    File.Create(file_path);
                }
            }
        }
    }
}