using Cloud.Framework.Assembly;

namespace Cloud.Dapper.Framework
{
    public abstract class DapperBase : CacheBaseAssembly
    {
        public override string AreaKey { get; } = "Cloud.Dapper";


    }
}