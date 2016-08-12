using Cloud.Framework.Assembly;

namespace Cloud.Dapper.Framework
{
    public abstract class DapperBase : CentralCacheAreaBase
    {
        public override string AreaKey { get; } = "Cloud.Dapper";


    }
}