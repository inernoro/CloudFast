using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Abp.Dependency;
using Abp.Domain.Entities;
using Cloud.Framework.Dapper;
using Newtonsoft.Json;

namespace Cloud.Framework.Redis
{
    public static class CacheExtension
    {

        private static IRedisHelper _redisHelper;

        private static readonly object Locker = new object();

        public static IRedisHelper Manager()
        {
            if (_redisHelper == null)
            {
                lock (Locker)
                {
                    if (_redisHelper != null)
                        return _redisHelper;
                    return _redisHelper = IocManager.Instance.Resolve<IRedisHelper>(10);

                }
            }
            return _redisHelper;
        }

        /// <summary>
        /// 获取某字段
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="func"></param>
        /// <returns></returns>

        public static string Get<TModel, TResult>(this TModel entity, Expression<Func<TModel, TResult>> func)
            where TModel : Entity
        {
            CheckModel(entity);
            var member = func.Body as MemberExpression;
            if (member == null)
                throw new Exception("表达式为空");
            var key = entity.GetRedisKey<TModel>();
            if (Manager().ExistsKey(key))
            {
                return Manager().HGet(key, member.Member.Name);
            }
            var data = IocManager.Instance.Resolve<IDapperHelper>().Get<TModel>(entity.Id);
            if (data != null)
            {
                SetModel(data);
            }
            return Manager().HGet(key, member.Member.Name);
        }

        /// <summary>
        /// 批量获取对象信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="idList"></param>
        /// <returns></returns>
        public static List<TModel> GetModelList<TModel>(this int[] idList) where TModel : Entity
        {
            return idList.ToList().GetModelList<TModel>();
        }

        /// <summary>
        /// 批量获取对象信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="idList"></param>
        /// <returns></returns>
        public static List<TModel> GetModelList<TModel>(this List<int> idList) where TModel : Entity
        {
            var keyArray = idList.GetRedisKey<TModel>();
            //第一次获取Key
            var list = new List<TModel>();
            //获取不存在的Key列表
            var outKey = new List<string>();
            var safeKey = new List<string>();
            var checkKey = CheckKey(idList, keyArray, ref outKey, ref safeKey);
            //如果有不存在的Key
            if (checkKey.Count != 0)
            {
                var model = IocManager.Instance.Resolve<IDapperHelper>().GetListForId<TModel>(checkKey);

                list.AddRange(model);
                for (var index = 0; index < outKey.Count; index++)
                {
                    //有修改
                    if (model.Count > index && model[index].Id == idList[index])
                    {
                        var maps = model[index].ToMap();
                        maps.Add(new KeyValueStruct(CloudKey.CloudRedisEntityItself,
                            JsonConvert.SerializeObject(model[index])));
                        Manager().HSet(outKey[index], maps);
                    }
                }
            }
            //存在的列表去掉不存在的列表
            if (keyArray.Count == outKey.Count)
            {
                return list;
            }
            var keys = safeKey;
            list.AddRange(keys.Select(node => Manager().HGet<TModel>(node)));
            return list;
        }


        /// <summary>
        /// 批量获取字段信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="idList"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<string> GetFieldAll<TModel, TResult>(this List<int> idList, Expression<Func<TModel, TResult>> func) where TModel : Entity
        {
            var member = func.Body as MemberExpression;
            if (member == null)
                throw new Exception("表达式为空");
            var field = member.Member.Name;
            var keyArray = idList.GetRedisKey<TModel>();
            //第一次获取Key 
            //获取不存在的Key列表
            var outKey = new List<string>();
            var safeKey = new List<string>();
            var checkKey = CheckKey(idList, keyArray, ref outKey, ref safeKey);
            //如果有不存在的Key
            if (checkKey.Count != 0)
            {
                var model = IocManager.Instance.Resolve<IDapperHelper>().GetListForId<TModel>(checkKey);
                for (var index = 0; index < outKey.Count; index++)
                {
                    var maps = model[index].ToMap();
                    maps.Add(new KeyValueStruct(CloudKey.CloudRedisEntityItself, JsonConvert.SerializeObject(model[index])));
                    Manager().HSet(outKey[index], maps);
                }
            }
            return keyArray.Select(node => Manager().HGet(node, field)).ToList();
        }

        /// <summary>
        /// 检查Key是否存在
        /// </summary>
        /// <param name="idList">Id列表</param>
        /// <param name="keyArray">Key列表</param>
        /// <param name="notSafeKey">不存在的Key列表</param>
        /// <returns>存在的Id列表</returns>
        public static List<int> CheckKey(List<int> idList, List<string> keyArray, ref List<string> notSafeKey, ref List<string> safeKey)
        {
            var list = new List<int>();
            notSafeKey = new List<string>();
            safeKey = new List<string>();
            var manager = Manager();
            for (var index = 0; index < keyArray.Count; index++)
            {
                if (!manager.ExistsKey(keyArray[index]))
                {
                    notSafeKey.Add(keyArray[index]);
                    list.Add(idList[index]);
                }
                else
                {
                    safeKey.Add(keyArray[index]);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TModel Get<TModel>(this TModel entity, Expression<Func<TModel, bool>> func) where TModel : Entity
        {
            return entity.GetModel();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TModel GetModel<TModel>(this TModel entity) where TModel : Entity
        {
            CheckModel(entity);
            var key = entity.GetRedisKey<TModel>();
            return GetModel<TModel>(key, entity.Id);
        }

        private static TModel GetModel<TModel>(string key, int id = 0) where TModel : Entity
        {
            if (Manager().ExistsKey(key))
            {
                return Manager().HGet<TModel>(key);
            }
            var data = IocManager.Instance.Resolve<IDapperHelper>().Get<TModel>(id);
            if (data == null)
                return null;
            SetModel(data);
            return Manager().HGet<TModel>(key);
        }

        /// <summary>
        /// 更新模型到RedisHash
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        public static void UpdateModel<TModel>(this TModel model) where TModel : Entity
        {
            model.SetModel();
        }

        /// <summary>
        /// 更新listModel
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="models"></param>
        public static void UpdateListModel<TModel>(this List<TModel> models) where TModel : Entity
        {
            foreach (var model in models)
            {
                SetModel(model);
            }
        }

        /// <summary>
        /// 添加模型到RedisHash
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        public static void SetModel<TModel>(this TModel model) where TModel : Entity
        {
            CheckModel(model);
            var map = model.ToMap();
            map.Add(new KeyValueStruct(CloudKey.CloudRedisEntityItself, JsonConvert.SerializeObject(model)));
            Manager().HSet(model.GetRedisKey<TModel>(), map);
        }


        public static void CheckModel<TModel>(this TModel model) where TModel : Entity
        {
            if (model == null || model.Id == 0)
                throw new Exception("实体不能为空，或者实体Id不能为空！");
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        public static void RemoveCache<TModel>(this TModel model) where TModel : Entity
        {
            CheckModel(model);
            Manager().Remove(model.GetRedisKey<TModel>());
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <typeparam name="TModel"></typeparam> 
        public static void RemoveCache<TModel>(int id) where TModel : Entity
        {
            if (id == 0)
                return;
            Manager().Remove(CloudKey.GetRedisKey<TModel>(id));
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <typeparam name="TModel"></typeparam> 
        public static void RemoveCache<TModel>(this IEnumerable<TModel> model) where TModel : Entity
        {
            foreach (var node in model)
            {
                node.RemoveCache();
            }
        }
    }
}
