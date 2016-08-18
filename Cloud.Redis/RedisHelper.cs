using System;
using System.Collections.Generic;
using System.Linq;
using Cloud.Framework.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cloud.Redis
{
    public class RedisHelper : IRedisHelper
    {
        #region 初始化

        private static ConnectionMultiplexer _redis;

        private static readonly object Locker = new object();

        private readonly IDatabase _database;

        public RedisHelper()
        {
            _database = Manager.GetDatabase(RedisConfig.Database);
        }

        public static ConnectionMultiplexer Manager
        {
            get
            {
                if (_redis == null)
                {
                    lock (Locker)
                    {
                        if (_redis != null) return _redis;

                        _redis = GetManager(RedisConfig.ConnectionString);
                        return _redis;
                    }
                }

                return _redis;
            }
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = RedisConfig.ConnectionString;
            }

            return ConnectionMultiplexer.Connect(connectionString);
        }

        #endregion

        #region String 存储
         
        public bool Set<T>(string key, T t)
        {
            return Set(key, JsonConvert.SerializeObject(t));
        }

        public bool Set(string key, string value)
        {
            return _database.StringSet(key, value);
        } 

        public T Get<T>(string key)
        {
            if (ExistsKey(key))
            {
                var t = JsonConvert.DeserializeObject<T>(Get(key));
                return t;
            }
            return default(T);
        }

        public string Get(string key)
        {
            return _database.StringGet(key);
        }
         
        public bool Remove(string key)
        {
            return _database.KeyDelete(key);
        }

        #endregion

        #region 其他

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ExistsKey(string key)
        {
            return _database.KeyExists(key);
        }

        /// <summary>
        /// 批量判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> ExistsKeyList(IEnumerable<string> key)
        {
            return key.Where(node => !ExistsKey(node)).ToList();
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        public void Expire(string key, TimeSpan? timeSpan)
        {
            _database.KeyExpire(key, timeSpan);
        }


        /// <summary>
        /// 清空缓存
        /// </summary>
        public void FlushDb()
        {
            //  Clear();
        }

        public void Clear()
        {
            var endpoints = Manager.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = Manager.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        #endregion

        #region

        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string HGet(string key, string value)
        {
            return _database.HashGet(key, value);
        }

        /// <summary>
        /// 获取所有对象
        /// </summary>
        /// <param name="key"></param> 
        /// <returns></returns>
        public T HGet<T>(string key)
        {
            var value = _database.HashGet(key, CloudKey.CloudRedisEntityItself);
            return JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// 设置某个字段值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="span"></param>
        public void HSet(string key, string hashField, string value, int span = RedisConfig.TimeDefaultValidTime)
        {
            _database.HashSet(key, hashField, value);
            SetExpire(key);

        }

        public void HDel(string key, string hashField)
        {
            _database.HashDelete(key, hashField);
        }

        /// <summary>
        /// 批量添加键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entry"></param>
        /// <param name="span"></param>
        public void HSet(string key, List<KeyValueStruct> entry, int span = RedisConfig.TimeDefaultValidTime)
        {
            var list = entry.Select(node => new HashEntry(node.Name, node.Value ?? "")).ToArray();
            _database.HashSet(key, list);
            SetExpire(key);
        }

        /// <summary>
        /// 设置有效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="span"></param>
        public void SetExpire(string key, int span = RedisConfig.TimeDefaultValidTime)
        {
            var date = DateTime.Now.AddSeconds(span);
            _database.KeyExpire(key, date);

        }

        /// <summary>
        /// 根据Key批量获取模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listKey"></param>
        /// <returns></returns>
        public List<T> HGetList<T>(IEnumerable<string> listKey)
        {
            var list = new List<T>();
            foreach (var node in listKey)
            {
                list.Add(HGet<T>(node));
            }
            return list;
        }

        #endregion

        #region List

        public void SetAdd(string key, string value)
        {
            _database.SetAdd(key, value);
        }

        public void SetPop(string key, string value)
        {
            _database.SetPop(key);
        }

        public void ListAdd(string key, string value)
        {
            _database.ListRightPush(key, value);
        }

        public List<string> ListRange(string key, int start, int end)
        {
            return _database.ListRange(key, start, end).Select(x => x.ToString()).ToList();
        }
        

        #endregion
    }
}