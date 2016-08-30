using Cloud.Domain;
using Cloud.Mongo.Framework;

namespace Cloud.Mongo.Manager
{
    public class ManagerMongoRepositories : MongoRepositories<Domain.Manager, int>, IManagerMongoRepositories
    {

    }
}