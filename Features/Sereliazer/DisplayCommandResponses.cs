using System.ComponentModel;

namespace EffectDisplay.Features.Sereliazer
{
    public class DisplayCommandResponses
    {
        [Description("The player sees this message if you have disabled the database or the file was not found, meaning it is not allowed to be used.")]
        public string MessageWnenDataBaseDisabled { get; set; } = "The specified server does not have this function.";
        [Description("The player sees this message when turning on the display component.")]
        public string MessageWhenPlayerEnabled { get; set; } = "You have <b>enabled</b> the display of active effects.";
        [Description("The player sees this message when turning off the display component.")]
        public string MessageWhenPlayerDisabled { get; set; } = "You have <b>disabled</b> the display of active effects.";
        [Description("The player sees this message when the server was unable to find the player component or the player himself (for example, a call from the Dedicated Server)")]
        public string MessageWhenErrorOccurred { get; set; } = "The player's effects display component was not found, or the player himself was not found.";
    }
}
