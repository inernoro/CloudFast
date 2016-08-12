using Abp.Dependency;

namespace Cloud.Framework.Redis
{
    public interface IQueryHub : ISingletonDependency
    {
        void Send(string groupName, string message);
    }
}