using System.Web.Mvc;
using Cloud.Web.Controllers;

namespace Cloud.Web.Areas.ApiManager.Controllers
{
    public class ManagerController : CloudControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult List1()
        {
            return View();
        }
    }
}