using System;
using System.Collections.Generic;
using System.Linq;
using Cloud.Framework.Cache.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cloud.Redis.Framework
{
    public class RedisHelper : IRedisHelper
    {
        private static ConnectionMultiplexer _redis;

        private static readonly object Locker = new object();

        public static ConnectionMultiplexer Manager
        {
            get
            {
                if (_redis == null)
                {
                    lock (Locker)
                    {
                        if (_redis != null) return _redis;
                        _redis = ConnectionMultiplexer.Connect(CacheConfigurage.ConnectionString);
                        return _redis;
                    }
                }

                return _redis;
            }
        }

        #region

        public string HashGet(string key, string hashField, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.HashGet(key, hashField);
        }

        public Dictionary<string, string> HashGetAll(string key, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            var date = redis.HashGetAll(key);
            return date.ToDictionary<HashEntry, string, string>(hashEntry => hashEntry.Name, hashEntry => hashEntry.Value);
        }

        public string[] HashGet(string key, string[] hashFields, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            var date = redis.HashGet(key, hashFields.Select(hashField => (RedisValue)hashField).ToArray());
            return date.Select(redisValue => (string)redisValue).ToArray();
        }



        public void HashSet<TType>(string key, TType type, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            var propertyLst = typeof(TType).GetProperties();
            var redisEntity = propertyLst.Select(property => new HashEntry(property.Name, property.GetValue(type).ToString())).ToArray();
            redis.HashSet(key, redisEntity);
        }

        public void HashSet(string key, string hashField, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            redis.HashSet(key, hashField, value);
        }


        public long HashIncrement(string key, string hashField, long value = 1, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.HashIncrement(key, value, value);
        }
        public long HashDecrement(string key, string hashField, long value = 1, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.HashDecrement(key, value, value);

        }
        public bool HashDelete(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.HashDelete(key, value);
        }

        #endregion

        #region All

        public void KeyDelete(string key, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            redis.KeyDelete(key);
        }

        public void KeyDelete(string[] key, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            var rediskey = key.Select(s => (RedisKey)s).ToArray();
            redis.KeyDelete(rediskey);
        }

        #endregion 

        #region String

        public string StringGet(string key, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.StringGet(key);
        }

        public bool StringSet(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.StringSet(key, value);
        }

        #endregion

        #region Set


        public bool SetAdd(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.SetAdd(key, value);
        }

        public bool SetRemove(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.SetRemove(key, value);
        }
        public long SetLength(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.SetLength(key);
        }

        public List<string> SetMembers(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.SetMembers(key).Select(x => x.ToString()).ToList();
        }
        public string SetPop(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.SetPop(key);
        }
        #endregion

        #region List

        public long ListRightPush(string key, string value, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.ListRightPush(key, value);
        }

        public string[] ListRange(string key, long start, long end, int database = 0)
        {
            var redis = Manager.GetDatabase(database);
            return redis.ListRange(key, start, end).Select(x => x.ToString()).ToArray();
        }


        #endregion
    }
}