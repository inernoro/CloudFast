using System.Collections.Generic;
using Cloud.Framework.Strategy;

namespace Cloud.Domain
{
    public interface IBuildCodeExcuteStrategy : IStrategy
    { 
        void ExcuteCode(IEnumerable<BuildTable> buildTables);

    }
}