using System;
using Abp.Domain.Entities;

namespace Cloud.Framework.Mongo
{
    public class NetMonitor : Entity<string>, IMongodbBase
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString().Trim('-');

        public string Ip { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime CreateTime { get; set; }

    }
}