using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using InventorySystem.Items.Usables.Scp244.Hypothermia;
using System.Collections.Generic;
using System.ComponentModel;

namespace EffectDisplay
{
    public class Config : IConfig
    {
        [Description("will the plugin be enabled")]
        public bool IsEnabled { get; set; } = true;
        [Description("Whether the message from the plugin will be visible (Helps if you find a bug)")]
        public bool Debug { get; set; } = false;
        [Description("time during which the text is displayed (0.9 seconds is suitable if the average ping is 80 ms)")]
        public float TextUpdateTime { get; set; } = (float)0.9;
        [Description("What form will the message about what effect the player has enabled")]
        public string EffectMessage { get; set; } = @"<align=left><size=13>Effect: {effect} is {type} will end {duration} with {intensivity} intensivity</size></align>";
        [Description("If you think about it, it's clear that these lines are inserted into {type}")]
        public string BadTypeWriting { get; set; } = "<color=red>Negative</color>";
        [Description("If you think about it, it's clear that these lines are inserted into {type}")]
        public string GoodTypeWriting { get; set; } = "<color=green>Positive</color>";
        [Description("If you think about it, it's clear that these lines are inserted into {type}")]
        public string MixedTypeWriting { get; set; } = "<color=#FB00FF>Mixed</color>";
        [Description("list of effects that will not be displayed (automatic here are the effects that are issued by the game as technical https://en.scpslgame.com/index.php?title=Status_Effects)")]
        public List<EffectType> BlackListEffect { get; set; } = new List<EffectType>()
        {
            EffectType.SoundtrackMute,
            EffectType.InsufficientLighting
        };
        [Description("how any effect will be displayed on the screen")]
        public Dictionary<EffectType, string> EffectNameDisplay { get; set; } = new Dictionary<EffectType, string>()
        {
            [EffectType.AmnesiaItems] = "AmnesiaItems",
            [EffectType.AmnesiaVision] = "AmnesiaVision",
            [EffectType.Asphyxiated] = "Asphyxiated",
            [EffectType.Bleeding] = "Bleeding",
            [EffectType.Blinded] = "Blinded",
            [EffectType.Burned] = "Burned",
            [EffectType.Concussed] = "Concussed",
            [EffectType.Corroding] = "Corroding",
            [EffectType.Deafened] = "Deafened",
            [EffectType.Decontaminating] = "Decontaminating",
            [EffectType.Disabled] = "Disabled",
            [EffectType.Ensnared] = "Ensnared",
            [EffectType.Exhausted] = "Exhausted",
            [EffectType.Flashed] = "Flashed",
            [EffectType.Hemorrhage] = "Hemorrhage",
            [EffectType.Invigorated] = "Invigorated",
            [EffectType.BodyshotReduction] = "BodyshotReduction",
            [EffectType.Poisoned] = "Poisoned",
            [EffectType.Scp207] = "Scp207",
            [EffectType.Invisible] = "Invisible",
            [EffectType.SinkHole] = "SinkHole",
            [EffectType.DamageReduction] = "DamageReduction",
            [EffectType.MovementBoost] = "MovementBoost",
            [EffectType.RainbowTaste] = "RainbowTaste",
            [EffectType.SeveredHands] = "SeveredHands",
            [EffectType.Stained] = "Stained",
            [EffectType.Vitality] = "Vitality",
            [EffectType.Hypothermia] = "Hypothermia",
            [EffectType.Scp1853] = "Scp1853",
            [EffectType.CardiacArrest] = "CardiacArrest",
            [EffectType.InsufficientLighting] = "InsufficientLighting",
            [EffectType.SoundtrackMute] = "SoundtrackMute",
            [EffectType.SpawnProtected] = "SpawnProtected",
            [EffectType.Traumatized] = "Traumatized",
            [EffectType.AntiScp207] = "AntiScp207",
            [EffectType.Scanned] = "Scanned",
            [EffectType.PocketCorroding] = "PocketCorroding",
            [EffectType.SilentWalk] = "SilentWalk",
            [EffectType.Strangled] = "Strangled",
            [EffectType.Ghostly] = "Ghostly"
        };
        [Description("defines the path to the database (do not change unless necessary")]
        public string PathToDatabase { get; set; } = "{ExiledConfigPath}/EffectDisplay/Player.db";
        [Description("use database for save user chose")]
        public bool DbUsing { get; set; } = true;
    }
}
