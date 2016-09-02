using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Cloud.ApiManager.Manager.Dtos;
using Cloud.Framework.Assembly;
using Cloud.Framework.Mongo;

namespace Cloud.ApiManager.Manager
{
    public interface IManagerAppService : IApplicationService
    {
        [ContentDisplay("获取最新")]
        Task<ListResultOutput<GetOutput>> GetBatch();

        [HttpGet]
        ViewDataMongoModel AllInterface();

        [HttpGet]
        ListResultOutput<OpenDocumentResponse> Interface(string actionName);

        [HttpGet]
        List<NamespaceDto> GetNamespace();

        [HttpGet]
        OpenDocumentResponse GetInfo(string input);


        Task<TestOutput> Test(TestInput input);

    }
}