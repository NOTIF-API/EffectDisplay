using LiteDB;

namespace EffectDisplay.Features.Serelization
{
    public class UserData
    {
        [BsonId]
        public string UserId { get; set; }

        public bool IsUsing { get; set; }
    }
}
