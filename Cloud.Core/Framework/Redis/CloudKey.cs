using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;

namespace Cloud.Framework.Redis
{
    public static class CloudKey
    {
        //全局Key
        public const string CloudMasterKey = "CloudMasterKey";

        public const string CloudRedisEntityItself = "__entityItself";

        /// <summary>
        /// 传入实体对象和Id获取Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetRedisKey<T>(int id)
        {
            var keytype = typeof(T).Name;
            return GetKey(keytype, id);
        }


        private static string GetKey(string keyType, int id)
        {
            return CloudMasterKey + ":" + keyType + ":" + id;
        }


        /// <summary>
        /// 根据 int列表批量获取信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<string> GetRedisKey<T>(this IEnumerable<int> id)
        {
            var keytype = typeof(T).Name;
            return id.Select(node => GetKey(keytype, node)).ToList();
        }

        /// <summary>
        /// 根据实体对象获取Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEntity"></param>
        /// <returns></returns>
        public static string GetRedisKey<T>(this Entity iEntity) where T : Entity
        {
            return GetRedisKey<T>(iEntity.Id);
        }



    }
}