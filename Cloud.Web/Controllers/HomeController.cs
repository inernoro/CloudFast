using System.Web.Mvc;

namespace Cloud.Web.Controllers
{
    public class HomeController : CloudControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}