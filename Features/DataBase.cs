using LiteDB;
using System;
using EffectDisplay.Features.Sereliazer;
using Exiled.API.Features;

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
        /// Finds out if the user exists and adds it to the database if not present 
        /// </summary>
        /// <param name="UserId">User id</param>
        private void GetUserExist(string UserId)
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
        /// Finds out whether the user exists and adds it to the database if not present with the specified <see cref="User.IsAllow"/> parameter
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <param name="IsAllow">Automatically set the <see cref="User.IsAllow"/> value</param>
        private void GetUserExist(string UserId, bool IsAllow = true)
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
                user.IsAllow = IsAllow;
                users.Update(user);
            }
            db.Commit();
        }
        /// <summary>
        /// Set <see cref="User.IsAllow"/> parametr
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <param name="allow">our argument</param>
        public void IsAllow(string UserId, bool allow)
        {
            GetUserExist(UserId, allow);
        }
        /// <summary>
        /// Get IsAllow parametr
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <returns></returns>
        public bool IsAllow(string UserId)
        {
            ILiteCollection<User> users = db.GetCollection<User>("users");
            User user = users.FindOne(x => x.UserId == UserId);
            Log.Debug(user);
            return user is null ? true : user.IsAllow;
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
