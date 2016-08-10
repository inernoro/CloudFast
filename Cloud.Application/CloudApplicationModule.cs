using System.Reflection;
using Abp.Modules;

namespace Cloud
{
    [DependsOn(typeof(CloudCoreModule))]
    public class CloudApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
