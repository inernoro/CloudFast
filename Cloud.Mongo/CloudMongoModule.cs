using System.Reflection;
using Abp.Modules;
using System.Configuration;
using Cloud.Framework.Mongo;

namespace Cloud.Mongo
{
    [DependsOn(typeof(CloudCoreModule))]

    public class CloudMongoModule : AbpModule
    {
        public override void PreInitialize()
        { 

#if DEBUG
            MongodbConfig.ConnectionString = "mongodb://root:KONGque00@10.5.1.244";
            //MongodbConfig.ConnectionString = "mongodb://127.0.0.1:27017";
#else
            MongodbConfig.ConnectionString = ConfigurationManager.ConnectionStrings["MangoDbConnection"].ConnectionString;
#endif
            MongodbConfig.Database = "CloudApiManager";

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
