using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Cloud
{
    [DependsOn(typeof(AbpWebApiModule), typeof(CloudApplicationModule))]
    public class CloudWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(CloudApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
