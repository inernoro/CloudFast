using System;
using Abp.Domain.Entities;

namespace Cloud.Domain
{
    public class BuildTable : Entity
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }

    }
}