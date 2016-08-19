using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Runtime.Session;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework.Redis;
using Jil;
using System.Collections;
using System.Collections.Generic;

namespace Cloud.Framework.Assembly
{
    public static class Helper
    {
        #region Core

        public static bool SignIn()
        {
            var session = IocManager.Instance.Resolve<IAbpSession>();
            return session.UserId != null;
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