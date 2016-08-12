using System.Reflection;
using Abp.Modules;

namespace Cloud.Strategy
{
    [DependsOn(typeof(CloudCoreModule))]

    public class CloudStrategyModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
