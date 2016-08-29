using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Cloud.ApiManager.Manager.Dtos;
using Cloud.Framework.Assembly;

namespace Cloud.ApiManager.Manager
{
    public interface IManagerAppService : IApplicationService
    {
        [ContentDisplay("获取单条")]
        GetOutput Get(GetInput input);

        [ContentDisplay("提交单条")]
        Task Post(PostInput input);

        [ContentDisplay("修改单条")]
        Task Put(PutInput input);

        [ContentDisplay("删除单条")]
        Task Delete(DeleteInput input);


        [ContentDisplay("获取最新")]
        Task<ListResultOutput<GetOutput>> GetBatch();

    }
}