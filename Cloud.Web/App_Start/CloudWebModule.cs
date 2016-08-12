using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing; 
using Abp.Modules;
using Abp.Web.Mvc;

namespace Cloud.Web
{
    [DependsOn(
        typeof(AbpWebMvcModule), 
        typeof(CloudApplicationModule), 
        typeof(CloudWebApiModule))]
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
