using System.Collections.Generic;

namespace Cloud.Framework.Assembly
{
    /// <summary>
    /// 程序集中央缓存管理
    /// </summary>

    public static class CacheAssembly
    {
        private static Dictionary<string, Dictionary<string, object>> _dictionary;

        private static readonly object CentralCacheAreaLocker = new object();
        static CacheAssembly()
        {

        }

        private static Dictionary<string, Dictionary<string, object>> GetValue()
        {
            if (_dictionary == null)
            {
                lock (CentralCacheAreaLocker)
                {
                    if (_dictionary != null) return _dictionary;

                    _dictionary = new Dictionary<string, Dictionary<string, object>>();
                    return _dictionary;
                }
            }

            return _dictionary;
        }

        /// <summary>
        /// 获取域下的所有缓存（Base调用）
        /// </summary>
        /// <param name="areaKey"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetAreaValue(string areaKey)
        {
            if (GetValue().ContainsKey(areaKey))
            {
                return GetValue()[areaKey];
            }
            return null;
        }

        /// <summary>
        /// 初始化域空间（Base调用）
        /// </summary>
        /// <param name="areaKey"></param>
        public static void InitAreaKey(string areaKey)
        {
            if (!GetValue().ContainsKey(areaKey))
            {
                GetValue()[areaKey] = new Dictionary<string, object>();
            }
        }

        private static readonly object Locker = new object();
        private static readonly object Locker2 = new object();
        /// <summary>
        /// 设置孙子节点 （Base调用）
        /// </summary>
        /// <param name="areaKey"></param>
        /// <param name="childrenKey"></param>
        /// <param name="obj"></param>
        public static void SetAreaValue(string areaKey, string childrenKey, object obj)
        {

            if (!GetValue().ContainsKey(areaKey))
            {
                lock (Locker)
                {
                    if (!GetValue().ContainsKey(areaKey))
                    {
                        GetValue()[areaKey] = new Dictionary<string, object>();
                    }
                }
            }

            if (!GetValue().ContainsKey(childrenKey))
            {
                lock (Locker2)
                {
                    if (!GetValue().ContainsKey(childrenKey))
                    {
                        GetValue()[areaKey][childrenKey] = new object();
                    }
                }
            }

            GetValue()[areaKey][childrenKey] = obj;

        }

        /// <summary>
        /// 获取孙子节点 （Base调用）
        /// </summary>
        /// <param name="areaKey"></param>
        /// <param name="childrenKey"></param>
        public static object GetAreaValue(string areaKey, string childrenKey)
        {
            if (GetValue().ContainsKey(areaKey) && GetValue()[areaKey].ContainsKey(childrenKey))
            {
                return GetValue()[areaKey][childrenKey];
            }
            return null;

        }
    }
}