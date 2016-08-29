using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Abp.Domain.Entities;
using MongoDB.Driver;

namespace Cloud.Mongo.Framework
{
    public static class Recursion<TEntity>
    {
        /// <summary>
        /// 递归构建Update操作串
        /// </summary>
        /// <param name="fieldList"></param>
        /// <param name="property"></param>
        /// <param name="propertyValue"></param>
        /// <param name="item"></param>
        /// <param name="father"></param>
        public static void GenerateRecursion(ICollection<UpdateDefinition<TEntity>> fieldList, PropertyInfo property, object propertyValue, TEntity item, string father)
        {
            //复杂类型
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && propertyValue != null)
            {
                //集合
                var list = propertyValue as IList;
                if (list != null)
                {
                    foreach (var sub in property.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (!sub.PropertyType.IsClass || sub.PropertyType == typeof(string)) continue;
                        var arr = list;
                        if (arr.Count <= 0) continue;
                        for (var index = 0; index < arr.Count; index++)
                        {
                            foreach (var subInner in sub.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                            {
                                if (string.IsNullOrWhiteSpace(father))
                                    GenerateRecursion(fieldList, subInner, subInner.GetValue(arr[index]), item, property.Name + "." + index);
                                else
                                    GenerateRecursion(fieldList, subInner, subInner.GetValue(arr[index]), item, father + "." + property.Name + "." + index);
                            }
                        }
                    }
                }
                //实体
                else
                {
                    foreach (var sub in property.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {

                        if (string.IsNullOrWhiteSpace(father))
                            GenerateRecursion(fieldList, sub, sub.GetValue(propertyValue), item, property.Name);
                        else
                            GenerateRecursion(fieldList, sub, sub.GetValue(propertyValue), item, father + "." + property.Name);
                    }
                }
            }
            //简单类型
            else
            {
                if (property.Name != "Id")//更新集中不能有实体键_id
                {
                    fieldList.Add(string.IsNullOrWhiteSpace(father)
                        ? Builders<TEntity>.Update.Set(property.Name, propertyValue)
                        : Builders<TEntity>.Update.Set(father + "." + property.Name, propertyValue));
                }
            }
        }

        /// <summary>
        /// 构建Mongo的更新表达式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<UpdateDefinition<TEntity>> GeneratorMongoUpdate(TEntity item)
        {
            var fieldList = new List<UpdateDefinition<TEntity>>();
            foreach (var property in typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                GenerateRecursion(fieldList, property, property.GetValue(item), item, string.Empty);
            }
            return fieldList;
        }
    }
}