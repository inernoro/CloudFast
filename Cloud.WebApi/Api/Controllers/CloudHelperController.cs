using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.WebApi.Controllers.Dynamic.Formatters;
using Cloud.ApiManager.Manager; 

namespace Cloud.Api.Controllers
{
    public class CloudHelperController : ApiController
    {
        private readonly IManagerAppService _managerAppService;

        public CloudHelperController(IManagerAppService managerAppService)
        {
            _managerAppService = managerAppService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> JavascriptHelper()
        {
            var script = await _managerAppService.CloudHelper();
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }
    }
}