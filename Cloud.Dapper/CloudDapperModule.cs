using System.Reflection;
using Abp.Modules;
using System.Configuration;
using Cloud.Framework.Dapper;

namespace Cloud.Dapper
{
    [DependsOn(typeof(CloudCoreModule))]

    public class CloudDapperModule : AbpModule
    {
        public override void PreInitialize()
        { 

#if DEBUG
            DapperConnection.MasterConnectionString = "Server=10.5.1.238;database=CloudPlatform;uid=sa;pwd=1qaz@WSX;Packet Size=8192;Max Pool Size=1000";
            DapperConnection.SlaveConnectionString = "Server=10.5.1.238;database=CloudPlatform;uid=sa;pwd=1qaz@WSX;Packet Size=8192;Max Pool Size=1000";
            //DapperConnection.MasterConnectionString = "Server=123.56.129.104;database=CloudPlatform;uid=e2eDeveloper;pwd=1qazXSW@1qazXSW@;Packet Size=8192;Max Pool Size=1000";
            //DapperConnection.SlaveConnectionString = "Server=123.56.129.104;database=CloudPlatform;uid=e2eDeveloper;pwd=1qazXSW@1qazXSW@;Packet Size=8192;Max Pool Size=1000";
#else
            DapperConnection.MasterConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            DapperConnection.SlaveConnectionString = ConfigurationManager.ConnectionStrings["Slave"].ConnectionString;
#endif

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
