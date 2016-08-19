using System.Reflection;
using Abp.Modules;
using Cloud.Framework;

namespace Cloud.Redis.Framework
{
    [DependsOn(typeof(CloudCoreModule))]

    public class CloudRedisModule : AbpModule
    {  
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}