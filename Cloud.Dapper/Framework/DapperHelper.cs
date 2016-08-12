using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Domain.Entities;
using Abp.Extensions;
using Cloud.Framework.Dapper;
using Dapper;

namespace Cloud.Dapper.Framework
{
    public class DapperHelper : DapperBase, IDapperHelper
    {

        public override string ChildrenNodeKey { get; } = "DapperHelper";

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllList<T>()
        {
            return BottomDapper.Query<T>("select * from " + typeof(T).Name);
        }

        /// <summary>
        /// 根据条件获取第一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FirstOrDefault<T>(string where = null)
        {
            return BottomDapper.QueryEnumerable<T>("select top 1 * from " + typeof(T).Name, null).FirstOrDefault();
        }

        /// <summary>
        /// 根据Id获取一条信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(int id)
        {
            var name = typeof(T).Name;
            return BottomDapper.QueryEnumerable<T>($"select top 1 * from [{name}] WHERE ID = " + id, null).FirstOrDefault();
        }

        /// <summary>
        /// 根据Id获取所有信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idList"></param>
        /// <returns></returns>
        public List<T> GetListForId<T>(IEnumerable<int> idList)
        {
            return GetListForPrimary<T>(idList, "ID");
        }

        /// <summary>
        /// 根据主键获取所有信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idList"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public List<T> GetListForPrimary<T>(IEnumerable<int> idList, string primaryKey)
        {
            var tableName = typeof(T).Name;
            return BottomDapper.Query<T>($"SELECT * FROM [{tableName}] WHERE {primaryKey} IN ( {string.Join(",", idList)} )");
        }

        /// <summary>
        /// 查询list列表，使用sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> QueryList<T>(string where)
        {
            return BottomDapper.Query<T>(where);
        }

        #region CUD

        public bool UpdateEntity<TModel>(TModel model) where TModel : Entity
        {
            var tableName = typeof(TModel).Name;
            return BottomDapper.ExecuteSql($"UPDATE [{tableName}] SET {UpdateSet<TModel>()} WHERE ID = {model.Id}", GetParament(model)) > 0;
        }

        private string UpdateSet<TModel>()
        {
            return GetValue(nameof(UpdateSet), typeof(TModel).Name, () => GetFieNameArray<TModel>()
                     .Aggregate(new StringBuilder(), (x, y) => x.Append(y + "=@" + y + ","))
                     .ToString()
                     .Trim(','));
        }

        public int CreateEntity<TModel>(TModel model) where TModel : Entity
        {
            var list = GetFieNameArray<TModel>();
            var sql = $"INSERT INTO [{typeof(TModel).Name}] ({string.Join(",", list)}) VALUES (@{string.Join(",@", list)});SELECT @@IDENTITY";
            return BottomDapper.QueryScalar<int>(sql, GetParament(model));
        }

        #region GetParament Test

        /// <summary>
        /// 根据对象获取DynamicParament
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public DynamicParameters GetParament<T>(T t)
        {
            var parament = new DynamicParameters();
            foreach (var node in typeof(T).GetProperties())
            {
                if (node.Name.ToLower() == "id")
                    continue;
                parament.Add("@" + node.Name, node.GetValue(t));
            }
            return parament;
        }
        #endregion

        /// <summary>
        /// 获取字段名字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<string> GetFieNameArray<T>()
        {
            // GetValue("第一个参数：这里是本方法的名字", () => "第二个参数：若缓存里没有这个数据，则执行这里的方法", "第三个可选参数：该方法的内操作字段的唯一Key");

            return GetValue(nameof(GetFieNameArray), typeof(T).Name,
                () => (from node in typeof(T).GetProperties() where node.Name.ToLower() != "id" select node.Name).ToList());

        }

        public bool DeleteEntity<TModel>(TModel model) where TModel : Entity
        {
            return DeleteEntity<TModel>(model.Id);
        }

        public bool DeleteEntity<TModel>(int id) where TModel : Entity
        {
            var tableName = typeof(TModel).Name;
            return BottomDapper.ExecuteSql($"delete from [{tableName}] where id = {id}") > 0;
        }

        /// <summary>
        /// 根据id列表批量删除数据，并且返回删除的个数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int DeleteList(string tableName, int[] idList)
        {
            return idList.Length == 0 || tableName.IsNullOrEmpty() ? 0
                : BottomDapper.ExecuteSql($"delete from [{tableName}] where id in @id", new { id = idList });
        }


        public List<TEntity> GetInfo<TEntity>(string tableName, int id, string primaryKey) where TEntity : Entity
        {
            return BottomDapper.Query<TEntity>($"SELECT * FROM [{tableName}] WHERE  {primaryKey} = {id}");
        }

        #endregion 


    }
}
