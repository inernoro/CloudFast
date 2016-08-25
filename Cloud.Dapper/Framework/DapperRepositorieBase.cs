using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Entities;
using Cloud.Framework.Dapper;

namespace Cloud.Dapper.Framework
{
    public abstract class DapperRepositorieBase<TEntity> : DapperRepositorie, IDapperRepositorie<TEntity> where TEntity : IEntity
    {
        #region query  

        public abstract List<TEntity> GetAllList(string where, object parament = null, string field = "*");

        public virtual Task<List<TEntity>> GetAllListAsync(string where, object parament = null, string field = "*")
        {
            return Task.FromResult(GetAllList(where, parament, field));
        }

        public abstract TEntity Get(int id);

        public virtual Task<TEntity> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }

        public abstract TEntity FirstOrDefault(string where, object parament = null, string field = "*");

        public virtual Task<TEntity> FirstOrDefaultAsync(string where, object parament = null, string field = "*")
        {
            return Task.FromResult(FirstOrDefault(where, parament, field));
        }

        #endregion

        #region insert

        public abstract TEntity Insert(TEntity entity);

        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        #endregion

        #region update

        public abstract TEntity Update(TEntity entity);

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        } 

        #endregion

        #region delete


        public virtual void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public abstract void Delete(IEnumerable<TEntity> entities);

        public virtual Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() => Delete(entity));
        }

        public abstract void Delete(int id);

        public virtual Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }

        public abstract void Delete(string where, object parament = null);

        public virtual async Task DeleteAsync(string where, object parament = null)
        {
            await Task.Run(() => Delete(where, parament));
        }

        #endregion

        #region Aggregation 

        public abstract int Count();

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public abstract int Count(string where, object parament = null);

        public virtual Task<int> CountAsync(string where, object parament = null)
        {
            return Task.FromResult(Count(where, parament));
        }

        #endregion 
    }
}