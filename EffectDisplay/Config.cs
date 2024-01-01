using Exiled.API.Enums;
using Exiled.API.Interfaces;
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
        [Description("defines the path to the database (do not change unless necessary")]
        public string PathToDatabase { get; set; } = "{ExiledConfigPath}/EffectDisplay/Player.db";
        [Description("use database for save user chose")]
        public bool DbUsing { get; set; } = true;
    }
}
