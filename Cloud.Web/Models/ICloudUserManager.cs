using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Cloud.Web.Models
{
    public interface ICloudUserManager : ITransientDependency
    {
        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);
    }
}