using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cloud.Domain;
using Cloud.Mongo.Framework;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Cloud.Mongo.Manager
{
    public class ManagerMongoRepositories : MongoRepositories<Domain.InterfaceManager, string>, IManagerMongoRepositories
    {
        /// <summary>
        /// 往测试数据中追加数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="addManager"></param>
        /// <returns></returns>
        public async Task AdditionalTestData(string url, TestManager addManager)
        {
            var data = Queryable().FirstOrDefault(x => x.Id == url);
            if (data == null)
                await Collection.InsertOneAsync(new InterfaceManager { Id = url, TestManager = new List<TestManager> { addManager } });
            else
                await Collection.FindOneAndUpdateAsync(x => x.Id == url, Builders<InterfaceManager>.Update.Push(x => x.TestManager, addManager));
        }
    }
}