using Abp;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Cloud.Domain;

namespace Cloud.User
{
    public class UserInfoAppService : AbpServiceBase, IUserInfoAppService
    {
        private readonly IEventBus _eventBus;

        public UserInfoAppService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }


        public void Get()
        {
            _eventBus.Trigger(new EntityChangedEventData<UserInfo>(new UserInfo()));
            _eventBus.Trigger(new EntityCreatedEventData<UserInfo>(new UserInfo()));
            _eventBus.Trigger(new EntityDeletedEventData<UserInfo>(new UserInfo()));
        }
    }
}