using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

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

        [Description("these lines will be displayed for each effect type separately, allowing you to customize them")]
        public Dictionary<string, string> EffectLine { get; set; } = new Dictionary<string, string>()
        {
            {"Mixed", "<size=12>%effect% is <color=\"purple\">%type% end after %time%" },
            {"Positive", "<size=12>%effect% is <color=\"green\">%type% end after %time%" },
            {"Negative", "<size=12>%effect% is <color=\"red\">%type% end after %time%" }
        };

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
        [Description("List of roles for which the effects display will not be displayed (the roles of the dead are ignored)")]
        public List<RoleTypeId> IgnoredRoles { get; set; } = new List<RoleTypeId>()
        {
            
        };

        /// <summary>
        /// Return effect name from <see cref="EffectTranslation"/> or <see cref="EffectType"/> as <see cref="string"></see>
        /// </summary>
        public string GetTranslation(EffectType effectType)
        {
            return EffectTranslation.ContainsKey(effectType) ? EffectTranslation[effectType] : effectType.ToString();
        }
    }
}