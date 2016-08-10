using System.Reflection;
using Abp.Modules;

namespace Cloud
{
    public class CloudCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
