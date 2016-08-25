using System.Web.Mvc;

namespace Cloud.Web.Areas.ApiManager
{
    public class ApiManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "ApiManager";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ApiManager_default",
                "ApiManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}