﻿using Exiled.API.Features;
using EffectDisplay.EventHandler;
using player = Exiled.Events.Handlers.Player;
using System.IO;
using EffectDisplay.Features;
using MEC;

namespace EffectDisplay
{
    public class Main: Plugin<Config>
    {
        public static Main Instance { get; private set; }
        private RoundEvent RoundEvent { get; set; } = null;
        internal DataBaseManager DataBaseManager { get; set; }

        public override void OnEnabled()
        {
            Instance = this;

            if (!Directory.Exists(Paths.Configs + "/EffectDisplay"))
            {
                Log.Warn("The path to the file with the database was not found, we are creating");
                Directory.CreateDirectory(Paths.Configs + "/EffectDisplay");
            }
            if (!File.Exists(this.Config.PathToDatabase.Replace("{ExiledConfigPath}", Paths.Configs)))
            {
                Log.Warn("file not found it will be created");
                File.Create(this.Config.PathToDatabase.Replace("{ExiledConfigPath}", Paths.Configs));
            }
            // if there are no files, then he has time to wait for them to appear
            Timing.CallDelayed(0.5f, () =>
            {
                DataBaseManager = new DataBaseManager();
            });
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
