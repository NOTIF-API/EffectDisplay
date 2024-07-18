using LiteDB;
using System;
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
        /// <summary>
        /// Checks the user for presence in the database and adds it if not found
        /// </summary>
        /// <param name="UserId">key for get user or set</param>
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
        /// Checks the user for presence in the database and adds it if not found
        /// </summary>
        /// <param name="UserId">key for get user or set</param>
        /// <param name="IsAllow">Automatically set the <see cref="User.IsAllow"/> value to the user</param>
        private void MemberCheck(string UserId, bool IsAllow = true)
        {
            ILiteCollection<User> users = db.GetCollection<User>("users");
            User user = users.FindOne(x => x.UserId == UserId);
            if (user == null)
            {
                user = new User();
                user.UserId = UserId;
                user.IsAllow = IsAllow;
                users.Upsert(user);
            }
            else
            {
                user.UserId = UserId;
                user.IsAllow = IsAllow;
                users.Update(user);
            }
            db.Commit();
        }
        /// <summary>
        /// Set IsAllow parament"/>
        /// </summary>
        /// <param name="userid">user steam id to update</param>
        /// <param name="allow">our argument</param>
        public void IsAllow(string userid, bool allow)
        {
            MemberCheck(userid, allow);
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
            return user == null ? false : user.IsAllow;
        }
        /// <summary>
        /// Clear all resources and save data base
        /// </summary>
        public void Dispose()
        {
            this.SaveData();
            db = null;
            Path = null;
        }

        private void SaveData()
        {
            db.Commit();
            db.Dispose();
        }
    }
}
