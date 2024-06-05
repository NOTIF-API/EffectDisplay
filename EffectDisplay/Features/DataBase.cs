using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffectDisplay.Features.Sereliazer;

namespace EffectDisplay.Features
{
    public class DataBase: IDisposable
    {
        private string Path;

        private LiteDatabase db;

        public DataBase() 
        {
            Path = System.IO.Path.Combine(Plugin.Instance.Config.PathToDataBase, Plugin.Instance.Config.DatabaseName);
            db = new LiteDatabase(Path);
        }

        private void MemberCheck(string UserId)
        {
            ILiteCollection<User> users = db.GetCollection<User>("users");
            User user = users.FindOne(x => x.UserId == UserId);
            if (user == null)
            {
                user = new User();
                user.UserId = UserId;
                users.Upsert(user);
            }

            db.Commit();
        }
        /// <summary>
        /// Set IsAllow parament"/>
        /// </summary>
        /// <param name="userid">user steam id to update</param>
        /// <param name="allow">our argument</param>
        public void SetAllowTo(string userid, bool allow)
        {
            ILiteCollection<User> users = db.GetCollection<User>("users");
            MemberCheck(userid);
            User user = users.FindOne(x => x.UserId == userid);
            user.IsAllow = allow;
            users.Update(user);
            db.Commit();
        }
        /// <summary>
        /// Get IsAllow parametr
        /// </summary>
        /// <param name="userid">user steam id</param>
        /// <returns></returns>
        public bool IsAllow(string userid)
        {
            ILiteCollection<User> users = db.GetCollection<User>("users");
            User user = users.FindOne(x => x.UserId == userid);
            if (user == null)
            {
                return true;
            }
            else
            {
                return user.IsAllow;
            }
        }

        public void Dispose()
        {
            this.SaveData();
            db = null;
            Path = null;
        }

        public void SaveData()
        {
            db.Commit();
            db.Dispose();
        }
    }
}
