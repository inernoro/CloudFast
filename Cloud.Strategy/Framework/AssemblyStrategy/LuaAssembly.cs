using System.Collections.Generic;
using System.IO;
using Castle.Core.Internal;
using Cloud.Framework.Assembly;
using Neo.IronLua;

namespace Cloud.Strategy.Framework.AssemblyStrategy
{
    public class LuaAssembly : StrategyBase, ILuaAssembly
    {
        public Dictionary<string, dynamic> Dictionary = new Dictionary<string, dynamic>();

        //"D:\\Temp\\LuaHelper\\{0}.lua";

        private string _paths = string.Empty;

        public void InitInitialization(string path)
        {
            if (_paths.IsNullOrEmpty())
            {
                _paths = path;
            }
        }

        /// <summary>
        /// 根据命名空间获取结果集
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public dynamic NamespaceGetValue(string fullName)
        {
            if (Dictionary.ContainsKey(fullName))
            {
                return Dictionary[fullName];
            }
            var dy = DynamicNamespaceGetValue(fullName);
            Dictionary.Add(fullName, dy);
            return dy;
        }

        /// <summary>
        /// 根据命名空间动态加载
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public dynamic DynamicNamespaceGetValue(string fullName)
        {
            return ExecuteScript(GetScript(string.Format(_paths, fullName.Replace(".", "\\"))));
        }

        /// <summary>
        /// 根据地址动态加载
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public dynamic AddressGetValue(string address)
        {
            return ExecuteScript(GetScript(address));
        }

        /// <summary>
        /// 执行脚本文件并返回结果集
        /// </summary>
        /// <param name="script">脚本</param>
        /// <returns></returns>
        public dynamic ExecuteScript(string script)
        {
            dynamic create = new Lua().CreateEnvironment();
            create.dochunk(script, "LuaHelper.lua");
            return create;
        }
         

        /// <summary>
        /// 根据地址获取脚本
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetScript(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        public void UpdateScriptAssembly()
        {
            Dictionary = new Dictionary<string, dynamic>();
        }
    }
}