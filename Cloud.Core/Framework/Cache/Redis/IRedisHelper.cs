using System.Collections.Generic;
using Abp.Dependency;

namespace Cloud.Framework.Cache.Redis
{
    public interface IRedisHelper : ISingletonDependency
    {
        #region

        string HashGet(string key, string hashField, int database = 0);

        Dictionary<string, string> HashGetAll(string key, int database = 0);

        string[] HashGet(string key, string[] hashFields, int database = 0);
         


        void HashSet<TType>(string key, TType type, int database = 0);

        void HashSet(string key, string hashField, string value, int database = 0);


        long HashIncrement(string key, string hashField, long value = 1, int database = 0);

        long HashDecrement(string key, string hashField, long value = 1, int database = 0);

        bool HashDelete(string key, string value, int database = 0);

        #endregion

        #region All

        void KeyDelete(string key, int database = 0);

        void KeyDelete(string[] key, int database = 0);

        bool KeyExists(string key, int database = 0);

        #endregion

        #region String

        string StringGet(string key, int database = 0);

        bool StringSet(string key, string value, int database = 0);

        #endregion

        #region Set


        bool SetAdd(string key, string value, int database = 0);

        bool SetRemove(string key, string value, int database = 0);

        long SetLength(string key, string value, int database = 0);

        List<string> SetMembers(string key, string value, int database = 0);

        string SetPop(string key, string value, int database = 0);

        #endregion

        #region List

        long ListRightPush(string key, string value, int database = 0);

        string[] ListRange(string key, long start, long end, int database = 0);

        #endregion

    }
}