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

        private readonly ISignin _signin;

        public AccountController(ISignin signin)
        {
            _signin = signin;
        }

        // GET: Account
        public async Task<ActionResult> Index()
        {
            await _signin.SignInAsync(new User
            {
                Id = 1,
                UserName = "123"
            });
            return View();
        }
    }
}