using System;
using Abp.Domain.Entities;

namespace Cloud.Domain
{
    public class BuildTable : Entity
    {
        public string Name { get; set; } 
        public int Xtype { get; set; }
        public string ColName { get; set; }
        public DateTime CreateTime { get; set; }

    }
}