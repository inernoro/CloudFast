using System.Configuration;
using System.Reflection;
using Abp.Modules;
using Cloud.Framework.Redis;

namespace Cloud.Redis
{
    [DependsOn(typeof(CloudCoreModule))]

    public class CloudRedisModule : AbpModule
    {
        public override void PreInitialize()
        {

#if DEBUG
            RedisConfig.ConnectionString = "127.0.0.1:6380";
            RedisConfig.Database = 0;
#else
            RedisConfig.ConnectionString = ConfigurationManager.ConnectionStrings["Abp.Redis.Cache"].ConnectionString;
#endif
            RedisConfig.ConnStrings = RedisConfig.ConnectionString.Split(':');

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}