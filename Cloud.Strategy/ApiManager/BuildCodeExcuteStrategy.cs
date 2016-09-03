using System.Collections.Generic;
using Cloud.Domain;
using Cloud.Strategy.Framework;

namespace Cloud.Strategy.ApiManager
{
    public class BuildCodeExcuteStrategy : StrategyBase, IBuildCodeExcuteStrategy
    {
         
        public void ExcuteBuild(Dictionary<string, string> fileDictionary)
        {

        }

        public void ExcuteBuild(IEnumerable<BuildTable> buildTables)
        {
            


        }
    }
}