using Exiled.API.Interfaces;
using System.ComponentModel;

namespace EffectDisplay
{
    public class Config : IConfig
    {
        [Description("will the plugin be enabled")]
        public bool IsEnabled { get; set; } = true;
        [Description("Whether the message from the plugin will be visible (Helps if you find a bug)")]
        public bool Debug { get; set; } = false;
        [Description("What form will the message about what effect the player has enabled")]
        public string EffectMessage { get; set; } = @"<align=left><size=13>Effect: {effect} is {type} will end {duration} with {intensivity} intensivity</size></align>";
        [Description("If you think about it, it's clear that these lines are inserted into {type}")]
        public string BadTypeWriting { get; set; } = "<color=red>Negative</color>";
        [Description("If you think about it, it's clear that these lines are inserted into {type}")]
        public string GoodTypeWriting { get; set; } = "<color=green>Positive</color>";
        [Description("If you think about it, it's clear that these lines are inserted into {type}")]
        public string MixedTypeWriting { get; set; } = "<color=#FB00FF>Mixed</color>";
    }
}
