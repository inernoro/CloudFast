using System;
using Abp.Domain.Entities;

namespace Cloud.Framework.Mongo
{
    public sealed class MobileHelperMongo : Entity<string>
    {
        public override string Id { get; set; }
        public string Value { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;

    }
}