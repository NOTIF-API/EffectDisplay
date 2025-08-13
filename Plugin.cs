using EffectDisplay.EventHandler;
using EffectDisplay.Features;
using Exiled.API.Features;

using System;
using System.IO;
using System.Threading.Tasks;

namespace EffectDisplay
{
    public class Plugin : Plugin<Configs>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "EffectDisplay";

        public override Version Version { get; } = new Version(2, 8, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public static Plugin Instance { get; private set; }

        public static DataBase data { get; private set; }

        private PlayerEvent Event { get; set; }

        private GithubUpdater iUpdater { get; set; }

        public static bool HintServiceMeowDetected { get; private set; } = false;

        public override void OnEnabled()
        {
            DBCondition();
            Instance = this;
            iUpdater = new GithubUpdater("NOTIF-API", "EffectDisplay");
            data = new DataBase();
            Event = new PlayerEvent();
            SubscribeEvents();
            base.OnEnabled();
            // Background update task.
            Task.Run(() =>
            {
                Version latest = iUpdater.Version;
                if (latest == null || latest == new Version(0, 0, 0)) return;
                Log.Debug($"Current version: [{this.Version.ToString()}] latest version detected is {latest.ToString()}");
                if (Version >= latest) return;
                Log.Warn("New version of EffectDisplay plugin found");
            });
        }
        public override void OnDisabled()
        {
            iUpdater = null;
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
        }

        protected void UnsubscribeEvents()
        {
            Log.Debug($"{nameof(UnsubscribeEvents)}: Unsubscribe from events.");
            Exiled.Events.Handlers.Player.Verified -= Event.OnVerefied;
        }
        /// <summary>
        /// Checks whether the conditions for working with the database are satisfied and restores missing files if necessary
        /// </summary>
        private void DBCondition()
        {
            if (!this.Config.DataBaseEnabled)
            {
                Log.Warn($"{nameof(DBCondition)}: Database usage has been disabled in the plugin configuration!");
                return;
            }
            else
            {
                string file_path = Path.Combine(this.Config.PathToDataBase, this.Config.DatabaseName);
                // if folder do not detected creating it with file
                if (!Directory.Exists(this.Config.PathToDataBase))
                {
                    Log.Warn($"{nameof(DBCondition)}: The directory with the database from EffectDisplay was not found, we are creating a directory and a file.");
                    Directory.CreateDirectory(this.Config.PathToDataBase);
                    File.Create(file_path).Close();
                    return;
                }
                if (!File.Exists(file_path))
                {
                    Log.Warn($"{nameof(DBCondition)}: The database file was not found.");
                    File.Create(file_path).Close();
                }
            }
        }
    }
}