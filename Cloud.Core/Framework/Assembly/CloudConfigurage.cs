using Abp.Dependency;

namespace Cloud.Framework.Assembly
{
    public static class CloudConfigurage
    {
        public static void StartInitializationType()
        {
            IocManager.Instance.Resolve<IStartupStrategy>().StartInitialization();
        }
    }
}