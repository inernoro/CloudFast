using System.Web.Mvc;
using Abp.Dependency;
using Cloud.Framework.Assembly;
using Cloud.Framework.Dapper;

namespace Cloud.Web.Controllers
{
    public class HomeController : CloudControllerBase
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
    }
}