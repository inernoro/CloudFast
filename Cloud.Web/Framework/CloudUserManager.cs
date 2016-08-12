using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Framework
{
    public class CloudUserManager : UserManager<IUser<long>, long>, ICloudUserManager
    {
        public override async Task<ClaimsIdentity> CreateIdentityAsync(IUser<long> user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);
            return identity;
        }

        public CloudUserManager(ICloudUserStore store)
            : base(store)
        {

        }


    }
}