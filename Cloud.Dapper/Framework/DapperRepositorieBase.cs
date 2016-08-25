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

        public abstract IEnumerable<TEntity> Query(string sql, object parament = null);
         

        public abstract List<TEntity> GetAllList(string where, object parament = null, string field = "*");

        public virtual Task<List<TEntity>> GetAllListAsync(string where, object parament = null, string field = "*")
        {
            return Task.FromResult(GetAllList(where, parament, field));
        }

        public virtual TEntity Get(int id, string field = "*")
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new AbpException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsync(int id, string field = "*")
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new AbpException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        public virtual TEntity Single(string where, object parament = null, string field = "*")
        {
            return FirstOrDefault(where, parament, field);
        }

        public virtual Task<TEntity> SingleAsync(string where, object parament = null, string field = "*")
        {
            return Task.FromResult(Single(where, parament, field));
        }

        public abstract TEntity FirstOrDefault(int id, object parament = null, string field = "*");

        public virtual Task<TEntity> FirstOrDefaultAsync(int id, object parament = null, string field = "*")
        {
            return Task.FromResult(FirstOrDefault(id));
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

        public abstract IEnumerable<TEntity> InsertList(IEnumerable<TEntity> list);


        public virtual int InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public virtual Task<int> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }

        public virtual int InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        public virtual Task<int> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }

        #endregion

        #region update

        public abstract TEntity Update(TEntity entity);

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual TEntity Update(int id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            Update(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(int id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            await UpdateAsync(entity);
            return entity;
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
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(int id);

        public virtual Task DeleteAsync(int id)
        {
            Delete(id);
            return Task.FromResult(0);
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