using Cloud.Framework.Strategy;

namespace Cloud.Framework.Script
{
    public interface ILuaAssembly : IStrategy
    {
        void InitInitialization(string path);

        /// <summary>
        /// 根据命名空间获取结果集
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        dynamic NamespaceGetValue(string fullName);

        /// <summary>
        /// 每次都动态加载
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        dynamic DynamicNamespaceGetValue(string fullName);

        /// <summary>
        /// 每次都动态加载
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        dynamic AddressGetValue(string address);

        /// <summary>
        /// 加载脚本文件并返回结果集
        /// </summary>
        /// <param name="script">脚本</param>
        /// <returns></returns>
        dynamic ExecuteScript(string script);


        /// <summary>
        /// 更新缓存
        /// </summary>
        void UpdateScriptAssembly();
    }
}