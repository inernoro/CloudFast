using Abp.Dependency;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Framework
{
    public interface ICloudUserStore : IUserStore<User, long>, ITransientDependency
    {

    }
}