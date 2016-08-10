using System.Web.Mvc;

namespace Cloud.Web.Controllers
{
    public class HomeController : CloudControllerBase
    {
        public ActionResult Index()
        {

            return View(AbpSession.UserId ?? 0);
        }
    }
}