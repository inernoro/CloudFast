using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Cloud.Framework.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cloud.Mongo.Framework
{
    public class MongoRepositories<TEntity, TPrimaryKey> : MongodbBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
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
        protected static IMongoCollection<TEntity> Collection
        {
            get
            {
                var entity = typeof(TEntity).Name;
                return Database.GetCollection<TEntity>(entity);
            }
        }

        public override IQueryable<TEntity> Queryable()
        {
            return Collection.AsQueryable();
        }


        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Queryable().FirstOrDefault(x => Equals(x.Id, id));
        }

        public override TEntity Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        public override IEnumerable<TEntity> InsertList(IEnumerable<TEntity> list)
        {
            Collection.InsertMany(list);
            return list ?? new List<TEntity>();
        }

        public override TEntity Update(TEntity entity)
        {
            Collection.UpdateOne(Builders<TEntity>.Filter.Eq("Id", entity.Id), Builders<TEntity>.Update.Combine(Recursion<TEntity>.GeneratorMongoUpdate(entity)));
            return entity;
        }

        public override void Delete(TPrimaryKey id)
        {
            var dictionary = new Dictionary<string, TPrimaryKey> { { "_id", id } };
            var query = new QueryDocument(dictionary);
            Collection.DeleteOneAsync(query);
        }

        public void Deletes(IEnumerable<TEntity> item)
        {
            var list = new List<WriteModel<TEntity>>();

            foreach (var iitem in item)
            {
                var queryDocument = new QueryDocument("_id", new ObjectId(typeof(TEntity).GetProperty("Id").GetValue(iitem).ToString()));
                list.Add(new DeleteOneModel<TEntity>(queryDocument));
            }
            Collection.BulkWriteAsync(list).Wait();
        }
    }
}
