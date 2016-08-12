using System;
using System.Collections.Generic;

namespace Cloud.Framework.Assembly
{
    /// <summary>
    /// 系统缓存操作类（内存存储）
    /// </summary>
    public abstract class CentralCacheAreaBase
    {
        /// <summary>
        /// 该程序集的唯一键
        /// </summary>
        public abstract string AreaKey { get; }
        /// <summary>
        /// 该类的唯一键
        /// </summary>
        public abstract string ChildrenNodeKey { get; }
        /// <summary>
        /// 获取该方法下所有的值
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public object GetValue(string modelName)
        {
            return CentralCacheArea.GetAreaValue(AreaKey, ChildrenNodeKey + ":" + modelName);
        }

        /// <summary>
        /// 获取该方法下所有的值，以键值对隔开
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, TModel> GetValue<TModel>(string key) where TModel : class
        {
            var value = CentralCacheArea.GetAreaValue(AreaKey, ChildrenNodeKey + ":" + key) as Dictionary<string, TModel> ?? new Dictionary<string, TModel>();
            return value;
        }


        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="modelName">方法名字</param>
        /// <param name="obj">该方法所有的值</param>
        public void SetValue(string modelName, object obj)
        {
            CentralCacheArea.SetAreaValue(AreaKey, ChildrenNodeKey + ":" + modelName, obj);
        }

        /// <summary>
        /// 设置方法List实值
        /// </summary>
        /// <typeparam name="TModel">添加类型</typeparam>
        /// <param name="methodName">方法名字</param>
        /// <param name="key">方法中的Key名字</param>
        /// <param name="value">方法总值</param>
        /// <param name="data">方法添加值</param>
        public void SetValue<TModel>(string methodName, string key, Dictionary<string, TModel> value, TModel data)
        {
            value.Add(key, data);
            SetValue(methodName, value);
        }

        /// <summary>
        /// 获取缓存,根据模型区方法唯一值
        /// </summary>
        /// <typeparam name="T">需插入的对象</typeparam>
        /// <param name="methodName">该方法唯一的名字</param>
        /// <param name="modelName">模型的名字</param>
        /// <param name="func">若缓存没有，设定读取方式</param>
        /// <returns></returns>
        public T GetValue<T>(string methodName, string modelName, Func<T> func) where T : class
        {
            var value = CentralCacheArea.GetAreaValue(AreaKey, ChildrenNodeKey + ":" + methodName) as Dictionary<string, T> ?? new Dictionary<string, T>();
            if (value.ContainsKey(modelName))
            {
                return value[modelName];
            }
            var data = func();
            value.Add(modelName, data);
            CentralCacheArea.SetAreaValue(AreaKey, ChildrenNodeKey + ":" + methodName, value);
            return data;
        }

    }
}