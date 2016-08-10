using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Cloud.EntityFramework.Repositories
{
    public abstract class CloudRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<CloudDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected CloudRepositoryBase(IDbContextProvider<CloudDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class CloudRepositoryBase<TEntity> : CloudRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected CloudRepositoryBase(IDbContextProvider<CloudDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
