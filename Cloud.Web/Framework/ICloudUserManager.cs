using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Dependency;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Framework
{
    public interface ICloudUserManager : ITransientDependency
    {
        Task<ClaimsIdentity> CreateIdentityAsync(IUser<long> user, string authenticationType);


    }
}