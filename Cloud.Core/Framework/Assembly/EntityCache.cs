using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Json;
using Cloud.Domain;
using Cloud.Framework.Redis;

namespace Cloud.Framework.Assembly
{
    public class EntityCache : Entity,
        IEventHandler<EntityCreatedEventData<Entity>>,
        IEventHandler<EntityChangedEventData<Entity>>,
        IEventHandler<EntityDeletedEventData<Entity>>,
        ITransientDependency
    {

        private readonly ScriptDomainService _scriptDomainService;

        public EntityCache(ScriptDomainService scriptDomainService)
        {
            _scriptDomainService = scriptDomainService;
        }


        public void HandleEvent(EntityCreatedEventData<Entity> eventData)
        {
            var result = _scriptDomainService.Physics.EntityCreatedEventData(eventData.Entity);
        }

        public void HandleEvent(EntityChangedEventData<Entity> eventData)
        {
            //IocManager.Instance.Resolve<INetWorkStrategy>().Send("SqlCode", new { sql, parament });
            var result = _scriptDomainService.Physics.EntityChangedEventData(eventData.Entity);

        }

        public void HandleEvent(EntityDeletedEventData<Entity> eventData)
        {
            var result = _scriptDomainService.Physics.EntityDeletedEventData(eventData.Entity);
        }
    }
}
