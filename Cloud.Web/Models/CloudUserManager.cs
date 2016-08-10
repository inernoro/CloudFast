using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Cloud.Web.Models
{
    public class CloudUserManager : UserManager<User, long>, ICloudUserManager
    {
        public override async Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
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