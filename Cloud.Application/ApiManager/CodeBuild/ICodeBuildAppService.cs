using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;

namespace Cloud.ApiManager.CodeBuild
{
    public interface ICodeBuildAppService : IApplicationService
    {
        void BuildAllCode();

        [HttpGet]
        void BuildCode(string tableName);


    }
}