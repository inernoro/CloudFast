using System.Web;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cloud.Web.Framework;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Framework
{
    public class Signin : Controller, ISignin
    {
        private readonly ICloudUserManager _cloudUserManager;

        public Signin(ICloudUserManager cloudUserManager)
        {
            _cloudUserManager = cloudUserManager;
        }

        private IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

        /// <summary>
        /// 系统登入
        /// </summary>
        /// <param name="user"></param> 
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public async Task SignInAsync(IUser<long> user, bool rememberMe = false)
        {
            var identity = await _cloudUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
            HttpContext.User = User;
        }

    }
}