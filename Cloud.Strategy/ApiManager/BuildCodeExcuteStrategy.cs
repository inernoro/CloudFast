using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Abp.Domain.Entities;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework.Assembly;
using Cloud.Strategy.Framework;
using Neo.IronLua;

namespace Cloud.Strategy.ApiManager
{
    public class BuildCodeExcuteStrategy : StrategyBase, IBuildCodeExcuteStrategy
    {

        // private static readonly string BuildFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "{0}.cs"; 

        private const string BuildFilePath = "E:\\Temp\\{0}.cs";

        public void ExcuteBuild(Dictionary<string, string> dictionary)
        {
            foreach (var node in dictionary)
            {
                var path = string.Format(BuildFilePath, node.Key);
                var newDri = path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
                if (!File.Exists(newDri))
                {
                    Directory.CreateDirectory(newDri);
                }
                var writer = new StreamWriter(path);
                writer.Write(node.Value);
                writer.Close();
            }
        }

        public void ExcuteCode()
        {
            var field = TableObject.GetTable("Id", "Name", "Age", "Item", "Sheck");
            var types = TableObject.GetTable(34, 35, 36, 48, 52, 56);
            Dictionary<string, string> str = Physics.BuildCode("Student", field, types);
            ExcuteBuild(str);
        }
    }


}