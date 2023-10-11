using EffectDisplay.Features.Serelization;
using Exiled.API.Features;
using LiteDB;
using System;

namespace EffectDisplay.Features
{
    public class DataBaseManager
    {
        private string path = Main.Instance.Config.PathToDatabase.Replace("{ExiledConfigPath}", Paths.Configs);

        private LiteDatabase db;

        public DataBaseManager()
        {
            db = new LiteDatabase(path);
        }
        [Obsolete("Method is depricated use a SaveState")]
        public void SaveMember(UserData user)
        {
            ILiteCollection<UserData> ltc = db.GetCollection<UserData>("userdata");
            UserData a = ltc.FindOne(x => x.UserId == user.UserId);
            if (a != null)
            {
                a.IsUsing = user.IsUsing;
                ltc.Update(a);
                return;
            }
            else
            {
                ltc.Upsert(user);
            }
            db.Commit();
        }
        public bool GetMemberChose(string userid)
        {
            ILiteCollection<UserData> ltc = db.GetCollection<UserData>("userdata");
            UserData user = ltc.FindOne(x => x.UserId == userid);
            if (user == null)
            {
                return false;
            }
            else
            {
                return user.IsUsing;
            }
        }
        public void SaveState(bool arg, string userid)
        {
            ILiteCollection<UserData> ltc = db.GetCollection<UserData>("userdata");
            UserData user = ltc.FindOne(x => x.UserId == userid);
            if (user == null)
            {
                user = new UserData();
                user.UserId = userid;
                user.IsUsing = arg; 
                ltc.Upsert(user);
            }
            else
            {
                ltc.Delete(user.UserId);
                user = new UserData();
                user.UserId = userid;
                user.IsUsing = arg;
                ltc.Upsert(user);
            }
            db.Commit();
        }
    }
}
