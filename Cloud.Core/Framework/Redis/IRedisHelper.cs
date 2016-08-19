using System;
using System.Collections.Generic;
using Abp.Dependency;

namespace Cloud.Framework.Redis
{
    public interface IRedisHelper : ISingletonDependency
    {
        #region String 存储

        /// <summary> 
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存建</param>
        /// <param name="t">缓存值</param>
        /// <returns></returns>
        bool Set<T>(string key, T t);


        bool Set(string key, string value);

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        string Get(string key);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        #endregion

        #region 其他

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ExistsKey(string key);

        /// <summary>
        /// 批量判断Key是否存在
        /// </summary>
        /// <param name="key">输入的Key</param>
        /// <returns></returns>
        List<string> ExistsKeyList(IEnumerable<string> key);

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        void Expire(string key, TimeSpan? timeSpan);


        /// <summary>
        /// 清空所有缓存（所有的缓存数据库）
        /// </summary>
        void FlushDb();

        #endregion


        #region

        string HGet(string key, string value);

        T HGet<T>(string key);

        void HSet(string key, string hashField, string value, int span = CacheConfigurage.TimeDefaultValidTime);

        void HDel(string key, string hashField);

        void HSet(string key, List<KeyValueStruct> entry, int span = CacheConfigurage.TimeDefaultValidTime);

        void SetExpire(string key, int span = CacheConfigurage.TimeDefaultValidTime);

        /// <summary>
        /// 根据Id列表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idList"></param>
        /// <returns></returns>
        List<T> HGetList<T>(IEnumerable<string> idList);
        #endregion

        void ListAdd(string key, string value);

        List<string> ListRange(string key, int start, int end);

    }
}