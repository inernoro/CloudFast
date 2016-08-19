using System.Collections.Generic;
using System.IO;
using Cloud.Framework.Assembly;
using Neo.IronLua;

namespace Cloud.Strategy.Framework.AssemblyStrategy
{
    public class LuaAssembly : StrategyBase, ILuaAssembly
    {
        private const string Paths = "D:\\Temp\\LuaHelper\\{0}.lua";

        public Dictionary<string, dynamic> Dictionary = new Dictionary<string, dynamic>();

        public dynamic GetSql(string fullName)
        {
            return Dictionary.ContainsKey(fullName) ? Dictionary[fullName] : FileLoad(fullName);
        }

        public dynamic FileLoad(string fullName)
        {
            var script = GetScript(fullName);
            var helper = new Lua();
            dynamic create = helper.CreateEnvironment();
            create.dochunk(script, "LuaHelper.lua");
            Dictionary.Add(fullName, create);
            return create;
        }

        private static string GetScript(string name)
        {
            using (var reader = new StreamReader(string.Format(Paths, name.Replace(".", "\\"))))
            {
                return reader.ReadToEnd();
            }
        }


        public void UpdateCache()
        {
            Dictionary = new Dictionary<string, dynamic>();
        }
    }
}