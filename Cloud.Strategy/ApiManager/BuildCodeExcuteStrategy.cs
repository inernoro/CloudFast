using System.Collections.Generic;
using Cloud.Domain;
using Cloud.Strategy.Framework;
using Neo.IronLua;

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

        public void ExcuteCode()
        {
            Dictionary<string, string> str = Physics.BuildCode(
                "Student");

        }
    }
}