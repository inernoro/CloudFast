using System.Collections.Generic;
using System.IO;
using Cloud.Domain;
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
        private readonly IManagerUrlStrategy _managerUrlStrategy;

        public StartupStrategy(ILuaAssembly luaAssembly, IManagerUrlStrategy managerUrlStrategy)
        {
            _luaAssembly = luaAssembly;
            _managerUrlStrategy = managerUrlStrategy;
        }

        public void StartInitialization()
        {
            // LuaType.RegisterTypeExtension(typeof(Cache)); 
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
            DocumentConfigurage.Database = "CloudPlatfrom";

            //地址配置(耦合太高,后期去掉)
            var testUrl = new LuaConfig(dataConfig.testUrl());
            _managerUrlStrategy.Init(new Dictionary<string, string>()
            {
                {"allInterface",testUrl.Url.allInterface.ToString() },
                { "@interface",testUrl.Url.@interface.ToString() },
                { "getNamespace",testUrl.Url.getNamespace.ToString() },
                { "loginUrl",testUrl.Url.loginUrl.ToString() }
            });


        }
    }
}