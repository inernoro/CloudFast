using Cloud.Framework.Assembly;
using Cloud.Framework.Strategy;

namespace Cloud.Strategy.Framework.AssemblyStrategy
{
    public class StartupStrategy : StrategyBase, IStartupStrategy
    {
        private readonly ILuaAssembly _luaAssembly;

        public StartupStrategy(ILuaAssembly luaAssembly)
        {
            _luaAssembly = luaAssembly;
        }

        public void StartInitialization()
        {
            _luaAssembly.FileLoad(System.AppDomain.CurrentDomain.BaseDirectory + "/Excute/DataConfig.lua");


        }
    }
}