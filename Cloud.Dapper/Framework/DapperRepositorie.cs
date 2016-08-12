using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Domain.Entities;
using Castle.Components.DictionaryAdapter;
using Cloud.Framework.Dapper;
using Cloud.Framework.Redis;
using Dapper;

namespace Cloud.Dapper.Framework
{

    public class DapperRepositorie<TEntity> : DapperRepositorieBase<TEntity> where TEntity : Entity
    {
        public string TableName { get; set; }

        public override string ChildrenNodeKey { get; } = "DapperRepositorieBase";

        public DapperRepositorie()
        {
            TableName = typeof(TEntity).Name;
        }


        public override IEnumerable<TType> Query<TType>(string sql, object parament = null)
        {
            return BottomDapper.QueryEnumerable<TType>(sql, parament);
        }

        public override int Excute(string sql, object parament = null)
        {
            return BottomDapper.ExecuteSql(sql, parament);
        }

        public override IEnumerable<TEntity> Query(string sql, object parament = null)
        {
            return BottomDapper.QueryEnumerable<TEntity>(sql, parament);
        }

        public override List<TEntity> GetAllList(string where, object parament = null, string field = "*")
        {
            return BottomDapper.QueryEnumerable<TEntity>($"select {field} from [" + TableName + "] " + where, parament).ToList();
        }

        public override TEntity FirstOrDefault(int id, object parament = null, string field = "*")
        {
            return BottomDapper.QueryEnumerable<TEntity>($"select top 1 {field} from [" + TableName + "] ", parament).FirstOrDefault();
        }

        public override TEntity Insert(TEntity entity)
        {
            List<string> list;
            var parament = GetParament(entity, out list);
            var sql = $"INSERT INTO [{TableName}] ({string.Join(",", list)}) VALUES (@{string.Join(",@", list)});SELECT @@IDENTITY";
            entity.Id = BottomDapper.QueryScalar<int>(sql, parament);
            entity.UpdateModel();
            return entity;
        }

        public override IEnumerable<TEntity> InsertList(IEnumerable<TEntity> list)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var node in list)
            {
                Insert(node);
                node.UpdateModel();
            }
            // ReSharper disable once PossibleMultipleEnumeration
            return list;
        }

        public override TEntity Update(TEntity entity)
        {
            List<string> list;
            var parament = GetParament(entity, out list);
            var agg = list.Aggregate(new StringBuilder(), (x, y) =>
            {
                x.Append(y);
                x.Append("=@");
                x.Append(y);
                x.Append(",");
                return x;
            });
            var sql = $"UPDATE [{TableName}] SET {agg.ToString().TrimEnd(',')} WHERE ID = {entity.Id}";
            BottomDapper.ExecuteSql(sql, parament);
            entity.UpdateModel();
            return entity;
        }

        public override void Delete(IEnumerable<TEntity> entities)
        {
            entities.RemoveCache();
            var sql = $"delete from [{ TableName }] where id in @Id";
            var list = entities.Select(x => x.Id);
            BottomDapper.ExecuteSql(sql, new { Id = list });
        }

        public override void Delete(int id)
        {
            CacheExtension.RemoveCache<TEntity>(id);
            BottomDapper.ExecuteSql($"delete from [{TableName}] where id = {id}");
        }

        public override void Delete(string where, object parament = null)
        {
            var sql = $"delete from [{ TableName }] {where}";
            Query<TEntity>(sql, parament).RemoveCache();
            BottomDapper.ExecuteSql(sql, parament);
        }

        public override int Count()
        {
            return BottomDapper.QueryScalar<int>("SELECT COUNT(1) FROM [" + TableName + "]");
        }

        public override int Count(string where, object parament = null)
        {
            return BottomDapper.QueryScalar<int>("SELECT COUNT(1) FROM [" + TableName + "] " + where, parament);
        }

        public override void ExecProc(string procName, object parament = null, Action func = null)
        {
            BottomDapper.ExecuteProc(procName, parament);
            func?.Invoke();
        }

        public override IEnumerable<TModel> ExecProc<TModel>(string procName, object parament, Action func = null)
        {
            return BottomDapper.QueryProc<TModel, object>(procName, parament);
        }

        public override TOutType ExecProc<TModel, TOutType>(string procName, object parament, Func<IEnumerable<TModel>, TOutType> func)
        {
            var data = BottomDapper.QueryProc<TModel, object>(procName, parament);
            return func(data);
        }

        public override TEntity FirstOrDefault(string where, object parament = null, string field = "*")
        {
            return BottomDapper.QueryEnumerable<TEntity>($" select top 1 {field} from [" + TableName + "] " + where, parament).FirstOrDefault();
        }

        public List<string> GetFieNameArray<T>()
        {
            return GetValue(nameof(GetFieNameArray), typeof(T).Name,
                () => (from node in typeof(T).GetProperties() where node.Name.ToLower() != "id" select node.Name).ToList());
        }

        /// <summary>
        /// 根据对象获取DynamicParament
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public DynamicParameters GetParament<T>(T t, out List<string> list)
        {
            list = new List<string>();
            var parament = new DynamicParameters();
            foreach (var node in typeof(T).GetProperties())
            {
                if (node.Name.ToLower() == "id" || node.GetValue(t) == null) continue;
                list.Add(node.Name);
                parament.Add("@" + node.Name, node.GetValue(t));
            }
            return parament;
        }



        #region Page

        public override List<TOutType> Pagination<TOutType>(
            string sql,
            int currentIndex,
            int pageSize,
            string translate = "*",
            string orderBy = "Id",
            object parament = null
            )
        {
            var excuteSql = GetPaginationSql(sql, currentIndex, pageSize, translate, orderBy);
            return BottomDapper.Query<TOutType>(excuteSql, parament).ToList();
        }

        public override PageEntity<TOutType> Pagination<TOutType>(string sql, int currentIndex, int pageSize, bool sumCount, string translate = "*", string orderBy = "Id", object parament = null)
        {
            var excuteSql = GetPaginationSql(sql, currentIndex, pageSize, translate, orderBy) + $";SELECT A.COUNT FROM ( SELECT COUNT(1) AS COUNT FROM {sql}) A";


            var Return = BottomDapper.QueryMultiple(excuteSql, parament,
                  read =>
                  {
                      var page = new PageEntity<TOutType>
                      {
                          EntityList = read.Read<TOutType>().ToList(),
                          Count = read.Read<int>().Single()
                      };
                      return page;
                  });
            return Return;
        }

        public static string GetPaginationSql(
            string sql,
            int currentIndex,
            int pageSize,
            string translate = "*",
            string orderBy = "Id")
        {
            var start = currentIndex == 0 || currentIndex == 1 ? 1 : ((currentIndex - 1) * pageSize) + 1;
            return $"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {orderBy})NewRow ,{translate} FROM {sql} ) AUS WHERE NewRow BETWEEN {start} AND {start + pageSize - 1}";
        }
        #endregion

        #region

        public override List<IEnumerable<object>> QueryMultiple(string sql, object p, params Type[] type)
        {
            var procMultiple = BottomDapper.QueryMultiple(sql,
                  p, userinfo =>
                  {
                      var list = new List<IEnumerable<object>>();
                      list.AddRange(type.Select(node => userinfo.Read(node)));
                      return list;
                  });

            return procMultiple.ToList();
        }

        #endregion
    }

    public class DapperRepositorie : IDapperRepositorie
    {
        #region 

        public List<IEnumerable<object>> QueryMultiple(string sql, object p, params Type[] type)
        {
            var procMultiple = BottomDapper.QueryMultiple(sql,
                  p, userinfo =>
                  {
                      List<IEnumerable<object>> list = new EditableList<IEnumerable<object>>();
                      foreach (var node in type)
                      {
                          list.Add(userinfo.Read(node));
                      }
                      return list;
                  });

            return procMultiple.ToList();
        }

        #endregion

        public IEnumerable<TType> Query<TType>(string sql, object parament = null)
        {
            return BottomDapper.QueryEnumerable<TType>(sql, parament);
        }

        public int Excute(string sql, object parament = null)
        {
            return BottomDapper.ExecuteSql(sql, parament);
        }

        public void ExecProc(string procName, object parament = null, Action func = null)
        {
            BottomDapper.ExecuteProc(procName, parament);
            func?.Invoke();
        }

        public IEnumerable<TModel> ExecProc<TModel>(string procName, object parament, Action func = null)
        {
            return BottomDapper.QueryProc<TModel, object>(procName, parament);
        }

        public TOutType ExecProc<TModel, TOutType>(string procName, object parament, Func<IEnumerable<TModel>, TOutType> func)
        {
            var data = BottomDapper.QueryProc<TModel, object>(procName, parament);
            return func(data);
        }

        public TOutType ExecProc<TModel, TOutType>(string procName, Func<IEnumerable<TModel>, TOutType> func)
        {
            return ExecProc(procName, null, func);
        }

        public List<TOutType> Pagination<TOutType>(
            string sql,
            int currentIndex,
            int pageSize,
            string translate = "*",
            string orderBy = "Id",
             object parament = null
            )
        {
            var excuteSql = GetPaginationSql(sql, currentIndex, pageSize, translate, orderBy);
            return BottomDapper.Query<TOutType>(excuteSql, parament).ToList();
        }

        public PageEntity<TOutType> Pagination<TOutType>(string sql, int currentIndex, int pageSize, bool sumCount, string translate = "*", string orderBy = "Id", object parament = null)
        {
            var excuteSql = GetPaginationSql(sql, currentIndex, pageSize, translate, orderBy) + $";SELECT A.COUNT FROM ( SELECT COUNT(1) AS COUNT FROM {sql}) A";


            var Return = BottomDapper.QueryMultiple(excuteSql, parament,
                  read =>
                  {
                      var page = new PageEntity<TOutType>
                      {
                          EntityList = read.Read<TOutType>().ToList(),
                          Count = read.Read<int>().Single()
                      };
                      return page;
                  });
            return Return;
        }

        public static string GetPaginationSql(
            string sql,
            int currentIndex,
            int pageSize,
            string translate = "*",
            string orderBy = "Id"
           )
        {
            var start = currentIndex == 0 || currentIndex == 1 ? 1 : ((currentIndex - 1) * pageSize) + 1;
            return $"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {orderBy})NewRow ,{translate} FROM {sql} ) AUS WHERE NewRow BETWEEN {start} AND {start + pageSize - 1}";
        }
    }
}