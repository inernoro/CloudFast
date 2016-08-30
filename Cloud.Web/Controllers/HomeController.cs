using System.Web.Mvc;
using Abp.Dependency;
using Cloud.Domain;
using Cloud.Framework.Assembly;
using Cloud.Framework.Dapper;
using Cloud.Framework.Mongo;

namespace Cloud.Web.Controllers
{
    public class HomeController : CloudControllerBase
    {
       
        private readonly IMongoRepositories<UserInfo, int> _mongoRepositories;

        public HomeController(IMongoRepositories<UserInfo, int> mongoRepositories)
        {
            _mongoRepositories = mongoRepositories;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}