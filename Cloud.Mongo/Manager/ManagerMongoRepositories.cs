using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloud.Domain;
using Cloud.Mongo.Framework;
using MongoDB.Driver;

namespace Cloud.Mongo.Manager
{
    public class ManagerMongoRepositories : MongoRepositories<InterfaceManager, string>, IManagerMongoRepositories
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

        /// <summary>
        /// 获取成功的，最后一条数据(待定)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="addManager"></param>
        /// <returns></returns>
        public async Task GetDefaultValue(string url, TestManager addManager)
        {
            var query = Builders<InterfaceManager>.Filter.Exists(x => x.Id == url);
           // Collection.

        }
    }
}