using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Policy;

namespace EffectDisplay
{
    public class Configs: IConfig
    {
        [Description("will the plugin be active?")]
        public bool IsEnabled { get; set; } = true;
        [Description("will information be displayed for the developer, will help when errors are detected")]
        public bool Debug { get; set; } = false;
        [Description("will a database be used")]
        public bool IsDatabaseUse { get; set; } = true;
        [Description("This message will be displayed on each line")]
        public string EffectLineMessage { get; set; } = "<size=12>%effect% is %type% end after %time% second's</size>";
        [Description("effects combining harm and benefit and nothing (207 and its anti)")]
        public string MixedEffect { get; set; } = "<color=purple>Mixed</color>";
        [Description("Only good effect")]
        public string GoodEffect { get; set; } = "<color=green>Positive</color>";
        [Description("Only bad effect")]
        public string BadEffect { get; set; } = "<color=red>Negative</color>";
        [Description("decomposes the text on the screen to change only to what is processed by align")]
        public string HintLocation { get; set; } = "<align=left>";
        [Description("defines a list of effects that the player will not see (the effects of the technical process are automatically hidden)")]
        public List<EffectType> BlackList { get; set; } = new List<EffectType>()
        {
            EffectType.InsufficientLighting,
            EffectType.SoundtrackMute
        };
        [Description("https://discord.com/channels/656673194693885975/1172647045237067788/1172647045237067788 determines the name of the effect from the existing list to the one you specify")]
        public Dictionary<EffectType, string> EffectTranslation { get; set; } = new Dictionary<EffectType, string>()
        {
            { EffectType.Blinded, "Blinded" }
        };
        [Description("defines the database name in the path (required at the end of .db)")]
        public string DatabaseName { get; set; } = "data.db";
        [Description("locates the database")]
        public string PathToDataBase { get; set; } = Path.Combine(Paths.Configs, "EffectDisplay");
    }
}
