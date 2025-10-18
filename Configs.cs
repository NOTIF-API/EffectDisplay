using CustomPlayerEffects;

using EffectDisplay.Features.Sereliazer;

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
        public bool DataBaseEnabled { get; set; } = true;
        [Description("the time period for which information is updated")]
        public float UpdateTime { get; set; } = 0.9f;
        [Description("these lines will be displayed for each effect type separately, allowing you to customize them")]
        public Dictionary<StatusEffectBase.EffectClassification, string> EffectLine { get; set; } = new Dictionary<StatusEffectBase.EffectClassification, string>()
        {
            {StatusEffectBase.EffectClassification.Mixed, $"<color=purple>%effect%</color> -> %time%/%duration% LVL: %intensity%" },
            {StatusEffectBase.EffectClassification.Positive, $"<color=green>%effect%</color> -> %time%/%duration% LVL: %intensity%" },
            {StatusEffectBase.EffectClassification.Negative, $"<color=red>%effect%</color> -> %time%/%duration% LVL: %intensity%" },
            {StatusEffectBase.EffectClassification.Technical, " " }
        };
        [Description("defines a list of effects that the player will not see (the effects of the technical process are hidden)")]
        public List<EffectType> BlackList { get; set; } = new List<EffectType>()
        {
            EffectType.InsufficientLighting,
            EffectType.SoundtrackMute,
            EffectType.FogControl
        };
        [Description("https://discord.com/channels/656673194693885975/1172647045237067788/1172647045237067788 determines the name of the effect from the existing list to the one you specify")]
        public Dictionary<EffectType, string> EffectTranslation { get; set; } = new Dictionary<EffectType, string>()
        {
            { EffectType.None, "UnkownEffect" }
        };
        [Description("defines the database name in the path (required at the end of .db)")]
        public string DatabaseName { get; set; } = "data.db";
        [Description("folder location current database")]
        public string PathToDataBase { get; set; } = Path.Combine(Paths.Configs, "EffectDisplay");
        [Description("List of roles for which the effects display will not be displayed (the roles of the dead are ignored without configs sets)")]
        public List<RoleTypeId> IgnoredRoles { get; set; } = new List<RoleTypeId>()
        {
            RoleTypeId.None,
            RoleTypeId.Spectator
        };
        [Description("Standard settings for displaying information, used in the absence of any supported Hint providers")]
        public NativeHintSettings NativeHintSettings { get; set; } = new NativeHintSettings();
        [Description("What text will the user see when hovering over a question mark in the settings?")]
        public string EnabledDisplayDescription { get; set; } = "Determines whether the display of enabled effects is enabled, replaces .display in the console";
        [Description("Will the plugin notify you of a new update")]
        public bool CheckForUpdate { get; set; } = true;
        [Description("SSS component ID for the main field item (do not duplicate with others)")]
        public int HeaderId { get; set; } = 2030;
        [Description("Display text when hovering over a question mark")]
        public string HeaderDescription { get; set; } = "Provides settings for Effect Display";
        [Description("SSS component ID for the TwoButton (do not duplicate with others)")]
        public int TwoButtonId { get; set; } = 2031;
        [Description("Name of TwoButton field")]
        public string TwoButtonLabel { get; set; } = "Time effect display";
        [Description("First option name")]
        public string TwoButtonEnabled { get; set; } = "ON";
        [Description("Second option name")]
        public string TwoButtonDisabled { get; set; } = "OFF";
        [Description("The player sees this message if you have disabled the database or the file was not found, meaning it is not allowed to be used.")]
        public string MessageWnenDataBaseDisabled { get; set; } = "The specified server does not have this function.";
        [Description("The player sees this message when turning on the display component.")]
        public string MessageWhenPlayerEnabled { get; set; } = "You have <b>enabled</b> the display of active effects.";
        [Description("The player sees this message when turning off the display component.")]
        public string MessageWhenPlayerDisabled { get; set; } = "You have <b>disabled</b> the display of active effects.";
        [Description("The player sees this message when the server was unable to find the player component or the player himself (for example, a call from the Dedicated Server)")]
        public string MessageWhenErrorOccurred { get; set; } = "The player's effects display component was not found, or the player himself was not found.";
        /// <summary>
        /// Return effect name from <see cref="EffectTranslation"/> if not found return <see cref="EffectType"/> as <see cref="string"></see>
        /// </summary>
        public string GetName(EffectType effectType)
        {
            string name = string.Empty;
            if (EffectTranslation.TryGetValue(effectType, out name))
            {
                return name;
            }
            return effectType.ToString();
        }
    }
}