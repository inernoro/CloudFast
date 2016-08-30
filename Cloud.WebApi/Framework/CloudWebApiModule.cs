using System.Web.Http;
using System.Web.Http.Cors;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Cloud.Framework
{
    [DependsOn(typeof(AbpWebApiModule), typeof(CloudApplicationModule))]
    public class CloudWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(System.Reflection.Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(CloudApplicationModule).Assembly, "app")
                .Build();
            var cors = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(cors);
        }
    }
}
