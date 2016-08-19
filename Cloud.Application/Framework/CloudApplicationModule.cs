using Abp.Modules;

namespace Cloud.Framework
{
    [DependsOn(typeof(CloudCoreModule))]
    public class CloudApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}
