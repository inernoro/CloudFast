using System.IO;
using System.Text;
using System.Web.Mvc;
using Cloud.ApiManager.CodeBuild;
using Cloud.Web.Controllers;
using Ionic.Zip;

namespace Cloud.Web.Areas.ApiManager.Controllers
{
    public class ManagerController : CloudControllerBase
    {
        private readonly ICodeBuildAppService _codeBuildAppService;

        public ManagerController(ICodeBuildAppService codeBuildAppService)
        {
            _codeBuildAppService = codeBuildAppService;
        }

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

        [HttpGet]
        public FileResult GetResult(string tableName)
        {
            var result = _codeBuildAppService.BuilDictionary(tableName);
            foreach (var node in result)
            {
                var data = Encoding.UTF8.GetBytes(node.Value);
                return File(data, "text/plain", node.Key);
            }
            return null;
        }

        public ActionResult FileStreamDownload1()
        {
            var path = Server.MapPath("~/Files/鹤冲天.zip");
            var fileStream = new FileStream(path, FileMode.Open);
            var zipInputStream = new ZipInputStream(fileStream);
            var entry = zipInputStream.GetNextEntry();
            return File(zipInputStream, "application/pdf", Url.Encode("entry.Name"));
        }
    }
}