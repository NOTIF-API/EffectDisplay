using System;
using System.IO;

using EffectDisplay.Features.Sereliazer;

using Exiled.API.Features;

using LiteDB;

namespace EffectDisplay.Features
{
    public class DataBase
    {
        public static bool TryGetData(string id, out User user)
        {
            user = null;
            string path = Plugin.Instance.Config.DataPath;
            if (!File.Exists(path)) return false;
            using (var db = new LiteDatabase(path))
            {
                try
                {
                    var collection = db.GetCollection<User>("users");
                    user = collection.FindById(id);
                }
                catch (Exception e)
                {
                    Log.Debug($"[TryGetData] Error when trying get data {e.Message}");
                }
            }
            return user != null;
        }

        public static bool TryUpdate(User user)
        {
            if (user == null) return false;
            string path = Plugin.Instance.Config.DataPath;
            if (!File.Exists(path)) return false;
            using (var db = new LiteDatabase(path))
            {
                try
                {
                    var collection = db.GetCollection<User>("users");
                    collection.Upsert(user);
                }
                catch (Exception e)
                {
                    Log.Debug($"[TryUpdate] Error when trying update user data {e.Message}");
                    return false;
                }
            }
            return true;
        }
    }
}
