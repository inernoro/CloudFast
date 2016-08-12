using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Domain.Entities;
using Castle.Core.Internal;

namespace Cloud.Framework.Redis
{
    public static class Transformations
    {
        /// <summary>
        /// 对象转KeyValue字典
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<KeyValueStruct> ToMap<T>(this T obj) where T : Entity
        {
            var map = new List<KeyValueStruct>();
            var t = typeof(T);
            var pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in pi)
            {
                var mi = p.GetGetMethod();
                if (mi != null && mi.IsPublic)
                {
                    map.Add(new KeyValueStruct
                    {
                        Name = p.Name,
                        Value = mi.Invoke(obj, null)?.ToString()
                    });
                }
            }
            return map.ToList();
        }


        /// <summary>
        /// KeyValue转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static T ToModel<T>(this List<KeyValueStruct> keyValues) where T : Entity, new()
        {
            var keyValueStructs = keyValues.ToArray();
            var model = new T();
            var propertyLst = typeof(T).GetProperties();
            foreach (var property in propertyLst)
            {
                //键  
                try
                {
                    var name = keyValueStructs.Find(x => x.Name == property.Name);
                    if (string.IsNullOrEmpty(name.Value))
                        continue;
                    property.SetValue(model, name.Value, new object[] { });

                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return model;
        }

        /// <summary>
        /// 数据校准，自动将数据前移，跳过孔洞
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList"></param>
        /// <param name="idList"></param>
        /// <returns></returns>
        public static List<T> ToDataCalibration<T>(this List<T> modelList, int[] idList) where T : Entity
        {
            return idList.Select(node => modelList.Find(x => x.Id == node)).Where(model => model != null).ToList();
        }

    }
}