using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;

namespace Cloud.ApiManager.CodeBuild
{
    public interface ICodeBuildAppService : IApplicationService
    {
        [HttpGet]
        void BuildCode(string tableName);



        [HttpGet]
        Dictionary<string, string> BuilDictionary(string tableName);
    }
}