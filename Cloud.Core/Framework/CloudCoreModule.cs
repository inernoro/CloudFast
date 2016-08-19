using Abp.Modules;

namespace Cloud.Framework
{
    public class CloudCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(System.Reflection.Assembly.GetExecutingAssembly());
          
        }
    }
}
