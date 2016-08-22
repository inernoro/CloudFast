using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Json;
using Cloud.Framework.Redis;

namespace Cloud.Framework.Assembly
{
    public static class Cache
    {
        public static void Call(Entity entity)
        {
            var redis = IocManager.Instance.Resolve<IRedisHelper>();
            redis.ListRightPush("testKey", entity.ToJsonString());
        }
    }
}