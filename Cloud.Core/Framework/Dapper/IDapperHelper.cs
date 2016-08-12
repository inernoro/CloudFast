using System.Collections.Generic;
using Abp.Dependency;
using Abp.Domain.Entities;

namespace Cloud.Framework.Dapper
{
    /// <summary>
    /// DapperHeler 帮助类
    /// </summary>
    public interface IDapperHelper : ISingletonDependency
    {
        List<T> GetAllList<T>();

        T FirstOrDefault<T>(string where = null);

        T Get<T>(int id);

        List<T> QueryList<T>(string where);

        List<T> GetListForId<T>(IEnumerable<int> idList);

        List<T> GetListForPrimary<T>(IEnumerable<int> idList, string primaryKey);

        bool UpdateEntity<TModel>(TModel model) where TModel : Entity;

        int CreateEntity<TModel>(TModel model) where TModel : Entity;

        bool DeleteEntity<TModel>(TModel model) where TModel : Entity;

        bool DeleteEntity<TModel>(int id) where TModel : Entity;

        int DeleteList(string tableName, int[] idList);

        List<TEntity> GetInfo<TEntity>(string tableName, int id, string primaryKey) where TEntity : Entity;

        List<string> GetFieNameArray<T>();


    }
}