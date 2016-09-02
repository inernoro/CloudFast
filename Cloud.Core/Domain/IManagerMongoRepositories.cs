using System.Threading.Tasks;
using Cloud.Framework.Mongo;

namespace Cloud.Domain
{
    public interface IManagerMongoRepositories : IMongoRepositories<InterfaceManager, string>
    {
        Task AdditionalTestData(string url, TestManager addManager);

    }
}