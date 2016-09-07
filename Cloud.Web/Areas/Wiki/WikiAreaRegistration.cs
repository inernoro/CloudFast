using System.Web.Mvc;

namespace Cloud.Web.Areas.Wiki
{
    public class WikiAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Wiki";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Wiki_default",
                "Wiki/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}