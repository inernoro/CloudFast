using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Runtime.Session;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework.Redis;
using Jil;

namespace Cloud.Framework.Assembly
{
    public static class ModelHelper
    {

        #region Core

        public static bool SignIn()
        {
            var session = IocManager.Instance.Resolve<IAbpSession>();
            return session.UserId != null;
        }

        public static T M<T>(int id) where T : Entity, new()
        {
            return new T { Id = id };
        }

        public static T Cache<T>(int? i) where T : Entity, new()
        {
            return new T { Id = i ?? 0 }.GetModel();
        }
        public static T Cache<T>(long? i) where T : Entity, new()
        {
            return new T { Id = Convert.ToInt32(i ?? 0) }.GetModel();
        }


        #endregion

        #region User

        public static UserInfo GetUser()
        {
            var session = IocManager.Instance.Resolve<IAbpSession>();
            return Convert.ToInt32(session.UserId) == 0 ? null : Cache<UserInfo>(session.UserId);
        }

        public static UserInfo GetUser(int id)
        {
            return Cache<UserInfo>(id);
        }

        #endregion

        #region Login

        public static void SignIn(Action func, string notLoadInfo = null)
        {
            if (!SignIn())
                NotLogin(notLoadInfo);
            func();
        }

        public static async Task SignIn(Action<UserInfo> func, string notLoadInfo = null)
        {
            if (!SignIn())
                NotLogin(notLoadInfo);
            await Task.Run(() => func(GetUser()));
        }

        public static T SignIn<T>(Func<T> func, string notLoadInfo = null)
        {
            if (!SignIn())
                NotLogin(notLoadInfo);
            return func();
        }

        public static T SignIn<T>(Func<UserInfo, T> func, string notLoadInfo = null)
        {
            if (!SignIn())
                NotLogin(notLoadInfo);
            return func(GetUser());
        }
        public static void NotLogin(string message)
        {
            if (message == null)
            {
                message = "您没有登陆，请登陆后再操作！";
            }
            throw new UserFriendlyException(message);
        }

        public static string Serialize<T>(this T t)
        {
            return JSON.Serialize(t);
        }

        public static T Deserialize<T>(this string str)
        {
            return JSON.Deserialize<T>(str);
        }

        #endregion
    }
}