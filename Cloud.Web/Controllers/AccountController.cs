using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cloud.Web.Framework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Cloud.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly ICloudUserManager _cloudUserManager;

        public AccountController(ICloudUserManager cloudUserManager)
        {
            _cloudUserManager = cloudUserManager;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        // GET: Account
        public async Task<ActionResult> Index()
        {
            await SignInAsync(new User()
            {
                Id = 1
                ,
                UserName = "123"
            });
            return View();
        }

        /// <summary>
        /// 系统登入
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
                identity = await _cloudUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
            HttpContext.User = User;
        }
    }
}