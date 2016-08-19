using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Cloud.Framework.Mongo;
using MongoDB.Driver;

namespace Cloud.Mongo.Framework
{
    public class MongodbHelper<TEntity, TPrimaryKey> : MongodbBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private static readonly string Conn = DocumentConfigurage.ConnectionString;

        private static readonly string DatabaseName = DocumentConfigurage.Database;


        /// <summary>
        /// 数据库
        /// </summary>
        private static IMongoDatabase Database => new MongoClient(Conn).GetDatabase(DatabaseName);

        /// <summary>
        /// 数据连接池
        /// </summary>
        private static IMongoCollection<TEntity> Collection
        {
            get
            {
                var entity = typeof(TEntity).Name;
                return Database.GetCollection<TEntity>(entity);
            }
        }
         
        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TEntity Get(TPrimaryKey id)
        {
            return Collection.Find(x => x.Id.Equals(id)).ToListAsync().Result[0];
        }

        /// <summary>
        /// 获取第一个模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Collection.Find(x => x.Id.Equals(id)).ToListAsync().Result[0];
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            Collection.InsertOneAsync(entity).Wait();
            return entity;
        }

        public override IEnumerable<TEntity> InsertList(IEnumerable<TEntity> list)
        {
            var insertList = list as TEntity[] ?? list.ToArray();
            Collection.InsertManyAsync(insertList).Wait();
            return insertList;
        }

        /// <summary>
        /// 修改，实现了两步操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TEntity Update(TEntity entity)
        {
            Delete(entity);
            Insert(entity);
            return entity;
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }
        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(TPrimaryKey id)
        {
            Collection.DeleteOneAsync(x => x.Id.Equals(id)).Wait();
        }

    }
}
