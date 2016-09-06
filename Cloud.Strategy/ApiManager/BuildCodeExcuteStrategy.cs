using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Abp.Domain.Entities;
using Cloud.Domain;
using Cloud.Framework.Assembly;
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
            var field = TableObject.GetTable("Id", "Name", "Age", "Item", "Sheck");
            var types = TableObject.GetTable(34, 35, 36, 48, 52, 56);
            Dictionary<string, string> str = Physics.BuildCode("Student", field, types);

        }
    }

   
}