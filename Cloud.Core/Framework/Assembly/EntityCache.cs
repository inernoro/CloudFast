using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Cloud.Domain;

namespace Cloud.Framework.Assembly
{
    public class EntityCache : Entity,
        IEventHandler<EntityCreatedEventData<Entity>>,
        IEventHandler<EntityChangedEventData<Entity>>,
        IEventHandler<EntityDeletedEventData<Entity>>,
        ITransientDependency
    {
        public void HandleEvent(EntityCreatedEventData<Entity> eventData)
        {

        }

        public void HandleEvent(EntityChangedEventData<Entity> eventData)
        {

        }

        public void HandleEvent(EntityDeletedEventData<Entity> eventData)
        {

        }
    }
}
