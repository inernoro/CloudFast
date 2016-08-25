using Abp.Collections;
using Abp.Modules;
using Abp.TestBase;
using Cloud.Dapper.Framework;
using Cloud.Framework;
using Cloud.Mongo.Framework;
using Cloud.Redis.Framework;
using Cloud.Strategy.Framework;

namespace Cloud.Test
{
    public abstract class TestBase : AbpIntegratedTestBase
    {

        protected TestBase()
        {

        }

        protected override void AddModules(ITypeList<AbpModule> modules)
        {
            base.AddModules(modules);
            modules.Add<CloudApplicationModule>();
            modules.Add<CloudCoreModule>();
            modules.Add<CloudRedisModule>();
            modules.Add<CloudMongoModule>();
            modules.Add<CloudDapperModule>();
            modules.Add<CloudTestModule>();
            modules.Add<CloudStrategyModule>();
        }

    }
}