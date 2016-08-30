using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using Abp.Owin;
using Cloud.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Cloud.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.MapSignalR();
            app.UseCors(CorsOptions.AllowAll);

            GlobalConfiguration.Configuration.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }

        private static bool IsTrue(string appSettingName)
        {
            return string.Equals(
                ConfigurationManager.AppSettings[appSettingName],
                "true",
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}