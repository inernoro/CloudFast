using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Caching;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;
using Cloud.Framework.Assembly;

namespace Cloud.Domain
{
    public class UserInfo : Entity
    {
        public override int Id { get; set; }
        public string Name { get; set; }
    }
}