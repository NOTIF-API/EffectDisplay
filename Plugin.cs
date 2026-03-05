using EffectDisplay.EventHandler;
using EffectDisplay.Features;
using Exiled.API.Features;

using System;
using System.IO;

namespace EffectDisplay
{
    public class Plugin : Plugin<Configs>
    {
        public override string Author { get; } = "notifapi";

        public override string Name { get; } = "EffectDisplay";

        public override Version Version { get; } = new Version(2, 9, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override bool IgnoreRequiredVersionCheck { get; } = false;

        public static Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            DBCondition();
            Instance = this;
            ServerSpecifiqManager.Init(Config);
            SubscribeEvents();
            base.OnEnabled();
            if (Config.CheckForUpdate)
            {
                Version ver = GithubUpdater.GetLatestAsync("NOTIF-API", "EffectDisplay").Result;
                if (ver == null || ver == new Version(0, 0, 0))
                {
                    Log.Debug($"[OnEnabled] Failed to check for updates.");
                    return;
                }
                else
                {
                    if (Version < ver)
                    {
                        Log.Warn($"A new version of the plugin is available: {ver}. You are using version {Version}. Download it from\nhttps://github.com/NOTIF-API/EffectDisplay/releases/latest");
                        return;
                    }
                }
            }
        }
        public override void OnDisabled()
        {
            ServerSpecifiqManager.Disable();
            UnsubscribeEvents();
            Instance = null;
            base.OnDisabled();
        }

        protected void SubscribeEvents()
        {
            Log.Debug($"[SubscribeEvents] Subscribe to the event.");
            Exiled.Events.Handlers.Player.Verified += PlayerEvent.OnVerefied;
        }

        protected void UnsubscribeEvents()
        {
            Log.Debug($"[UnsubscribeEvents] Unsubscribe from events.");
            Exiled.Events.Handlers.Player.Verified -= PlayerEvent.OnVerefied;
        }

        // Check directory and file existance for work and create if not exist
        private void DBCondition()
        {
            if (!this.Config.DataBaseEnabled)
            {
                Log.Warn($"[DBCondition] Database usage has been disabled in the plugin configuration!");
                return;
            }
            else
            {
                string file_directory = Path.GetDirectoryName(Config.DataPath);
                if (!Directory.Exists(file_directory)) Directory.CreateDirectory(file_directory);
                if (!File.Exists(Config.DataPath)) File.Create(Config.DataPath).Close();
            }
        }
    }
}