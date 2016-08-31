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
        private readonly ICurrentUser _currentUser;

        public HomeController(
            IMongoRepositories<UserInfo, int> mongoRepositories,
            ICurrentUser currentUser)
        {
            _mongoRepositories = mongoRepositories;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            //  var value = _currentUser.Token;
            return View();
        }
    }
}