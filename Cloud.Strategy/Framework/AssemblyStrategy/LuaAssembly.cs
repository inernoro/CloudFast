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
            return Dictionary.ContainsKey(fullName)
                ? Dictionary[fullName]
                : DynamicNamespaceGetValue(fullName);
        }

        /// <summary>
        /// 每次都动态加载
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public dynamic DynamicNamespaceGetValue(string fullName)
        {
            return FileLoad(GetScript(string.Format(_paths, fullName.Replace(".", "\\"))));
        }

        /// <summary>
        /// 每次都动态加载
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public dynamic AddressGetValue(string address)
        {
            return FileLoad(GetScript(address));
        }

        /// <summary>
        /// 加载脚本文件并返回结果集
        /// </summary>
        /// <param name="script">脚本</param>
        /// <returns></returns>
        public dynamic FileLoad(string script)
        {
            dynamic create = new Lua().CreateEnvironment();
            create.dochunk(script, "LuaHelper.lua");
            Dictionary.Add(script, create);
            return create;
        }


        /// <summary>
        /// 更新缓存
        /// </summary>
        public void UpdateCache()
        {
            Dictionary = new Dictionary<string, dynamic>();
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
    }
}