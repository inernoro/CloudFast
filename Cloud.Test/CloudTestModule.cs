using Abp.Modules;
using Cloud.Framework;
using Cloud.Redis.Framework;

namespace Cloud.Test
{
    [DependsOn(typeof(CloudCoreModule), typeof(CloudApplicationModule), typeof(CloudRedisModule))]
    public class CloudTestModule : AbpModule
    {
        public override void PreInitialize()
        {

        }
    }
}
