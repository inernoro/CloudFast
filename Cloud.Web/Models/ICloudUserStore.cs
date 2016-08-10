using Abp.Dependency;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Models
{
    public interface ICloudUserStore : IUserStore<User, long>, ITransientDependency
    {

    }
}