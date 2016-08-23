using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Modules;
using Abp.Web.Mvc;
using Cloud.Dapper.Framework;
using Cloud.Framework;
using Cloud.Mongo.Framework;
using Cloud.Redis.Framework;
using Cloud.Strategy.Framework;

namespace Cloud.Web
{
    [DependsOn(
        typeof(CloudWebApiModule),
        typeof(AbpWebMvcModule),
        typeof(CloudMongoModule),
        typeof(CloudRedisModule),
        typeof(CloudDapperModule),
        typeof(CloudApplicationModule),
        typeof(CloudStrategyModule))]
    public class CloudWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
