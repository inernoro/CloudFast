using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using Cloud.EntityFramework;

namespace Cloud
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(CloudCoreModule))]
    public class CloudDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<CloudDbContext>(null);
        }
    }
}
