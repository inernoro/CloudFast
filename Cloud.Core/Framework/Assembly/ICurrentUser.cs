using Abp.Dependency;

namespace Cloud.Framework.Assembly
{
    public interface ICurrentUser : ISingletonDependency
    {
        int Id { get; }
        string UserName { get; }
        string Password { get; }
        string Token { get;  }
    }
}