using System.Reflection;
using Abp.Modules;
using Cloud.Framework;

namespace Cloud.Dapper.Framework
{
    [DependsOn(typeof(CloudCoreModule))]

    public class CloudDapperModule : AbpModule
    { 
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
