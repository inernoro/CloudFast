using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public void ExcuteBuildReturn(Dictionary<string, string> dictionary)
        {

        }




        public void ExcuteCode(IEnumerable<BuildTable> paBuildTables)
        {
            var buildTables = paBuildTables.ToList();
            var tableName = buildTables.Select(x => x.Name).Distinct();
            foreach (var node in tableName)
            {
                var f = buildTables.FindAll(x => x.Name == node);
                var field = f.Select(x => x.ColName).ToArray();
                var types = f.Select(x => x.Xtype).ToArray();
                GetBuild(node, field, types);
            }
        }

        public void GetBuild(string tableName, string[] a, int[] b)
        {
            var field = TableObject.GetTable(a);
            var types = TableObject.GetTable(b);
            Dictionary<string, string> str = Physics.BuildCode(tableName, field, types);
            ExcuteBuild(str);
        }
    }


}