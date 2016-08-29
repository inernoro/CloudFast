using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Uow;

namespace Cloud.Framework.Mongo
{
    public interface IMongoRepositories<TEntity, in TPrimaryKey> : ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {

        #region Select/Get/Query 
        IQueryable<TEntity> GetAll(); 
        List<TEntity> GetAllList(); 
        Task<List<TEntity>> GetAllListAsync(); 
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate); 
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate); 
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);  
        TEntity FirstOrDefault(TPrimaryKey id); 
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id); 
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate); 
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate); 
        #endregion 
        #region Insert 

        TEntity Insert(TEntity entity); 
        Task<TEntity> InsertAsync(TEntity entity); 
        IEnumerable<TEntity> InsertList(IEnumerable<TEntity> list);  

        #endregion

        #region Update 
        TEntity Update(TEntity entity); 
        Task<TEntity> UpdateAsync(TEntity entity); 

        #endregion

        #region Delete 
        void Delete(TEntity entity); 
        Task DeleteAsync(TEntity entity); 
        void Delete(TPrimaryKey id); 
        Task DeleteAsync(TPrimaryKey id); 
        void Delete(Expression<Func<TEntity, bool>> predicate); 
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregates 
        int Count(); 
        Task<int> CountAsync(); 
        int Count(Expression<Func<TEntity, bool>> predicate); 
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate); 
        long LongCount(); 
        Task<long> LongCountAsync(); 
        long LongCount(Expression<Func<TEntity, bool>> predicate); 
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

    }
}