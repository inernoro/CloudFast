using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Json;
using Cloud.Framework.Cache.Redis;

namespace Cloud.Framework.Cache
{
    public static class Cache
    {
        public static void Call(Entity entity)
        {
            var redis = IocManager.Instance.Resolve<IRedisHelper>();
            redis.ListRightPush("testKey", entity.ToJsonString());
        }
    }

    public static class Cache1
    {
        public static void Call(Entity entity)
        {
            var redis = IocManager.Instance.Resolve<IRedisHelper>();
            redis.ListRightPush("testKey", entity.ToJsonString());
        }
    }

    public class Cache2
    {
        public static void Call(Entity entity)
        {
            var redis = IocManager.Instance.Resolve<IRedisHelper>();
            redis.ListRightPush("testKey", entity.ToJsonString());
        }
    }
}