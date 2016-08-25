using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;

namespace Cloud.Framework.Dapper
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDapperRepositorie<TEntity> : IDapperRepositorie where TEntity : IEntity
    {

        #region Select/Get/Query 
         

        List<TEntity> GetAllList(string where = null, object parament = null, string field = "*");

        Task<List<TEntity>> GetAllListAsync(string where = null, object parament = null, string field = "*");

        TEntity Get(int id);

        Task<TEntity> GetAsync(int id); 



        #endregion

        #region Insert

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);     

        #endregion

        #region Update

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        #endregion

        #region Delete

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        void Delete(int id);

        Task DeleteAsync(int id);

        void Delete(string where, object parament = null);

        Task DeleteAsync(string where, object parament = null);

        #endregion

        #region Aggregates

        int Count();

        Task<int> CountAsync();

        int Count(string where, object parament = null);

        Task<int> CountAsync(string where, object parament = null);

        #endregion 

    }

    /// <summary>
    /// 轻仓储（无状态）
    /// </summary>
    public interface IDapperRepositorie : ISingletonDependency
    {


        IEnumerable<TType> Query<TType>(string sql, object parament = null);

        int Excute(string sql, object parament = null);

        #region Proc

        void ExecProc(string procName, object parament = null, Action func = null);

        IEnumerable<TModel> ExecProc<TModel>(string procName, object parament = null, Action func = null);

        TOutType ExecProc<TModel, TOutType>(string procName, object parament, Func<IEnumerable<TModel>, TOutType> func);

        TOutType ExecProc<TModel, TOutType>(string procName, Func<IEnumerable<TModel>, TOutType> func);

        PageEntity<TOutType> Pagination<TOutType>(
            string sql,
            int currentIndex,
            int pageSize,
            bool sumCount,
            string translate = "*",
            string orderBy = "Id",
             object parament = null
            );


        List<TOutType> Pagination<TOutType>(
            string sql,
            int currentIndex,
            int pageSize,
            string translate = "*",
            string orderBy = "Id",
           object parament = null
            );


        List<IEnumerable<object>> QueryMultiple(string sql, object p, params Type[] type);

        #endregion
    }

    public enum Orderby
    {
        Desc,
        Asc
    }
}