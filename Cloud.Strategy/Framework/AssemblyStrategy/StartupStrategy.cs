using System.IO;
using Cloud.Framework.Assembly;
using Cloud.Framework.Dapper;
using Cloud.Framework.Mongo;
using Cloud.Framework.Redis;
using Cloud.Framework.Strategy;
using Neo.IronLua;

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
            var main = _luaAssembly.AddressGetValue(System.AppDomain.CurrentDomain.BaseDirectory + "Excute\\main.lua").main;
            var dataConfig = _luaAssembly.AddressGetValue(System.AppDomain.CurrentDomain.BaseDirectory + "Excute\\DataConfig.lua").dataConfig;
            //系统文件
            var system = new LuaConfig(main.start());
            _luaAssembly.InitInitialization(system.Url);
            //持久层
            var config = new LuaConfig(dataConfig.persistent());
            var sqlpath = config.Url;
            PersistentConfigurage.MasterConnectionString = sqlpath.master;
            PersistentConfigurage.SlaveConnectionString = sqlpath.slave;
            //缓存层
            var redisConfig = new LuaConfig(dataConfig.cache());
            CacheConfigurage.ConnectionString = redisConfig.Url.ToString();
            //聚合层
            var mongodbConfig = new LuaConfig(dataConfig.document());
            DocumentConfigurage.ConnectionString = mongodbConfig.Url.ToString();

            LuaType.RegisterTypeExtension(typeof(Cache));

        }
    }
}