using Cloud.Framework.Assembly;

namespace Cloud.Strategy.Framework
{
    public class CloudAssembly : StrategyBase, ICloudAssembly
    {
        public override string[] Declare => new[] { "/Framework/" };



    }
}