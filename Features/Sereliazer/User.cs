using LiteDB;

namespace EffectDisplay.Features.Sereliazer
{
    public class User
    {
        [BsonId]
        public string UserId { get; set; } = string.Empty;

        public bool IsAllow { get; set; } = true;
    }
}
