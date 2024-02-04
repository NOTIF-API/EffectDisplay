using LiteDB;

namespace EffectDisplay.Features.Serelization
{
    public class UserData
    {
        [BsonId]
        public string UserId { get; set; }
        /// <summary>
        /// if true, then the user is against the plugin’s action being visible on the screen
        /// </summary>
        public bool IsUsing { get; set; }
    }
}
