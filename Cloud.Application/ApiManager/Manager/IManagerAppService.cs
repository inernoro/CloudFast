using System.Threading.Tasks;
using Abp.Application.Services;
using Cloud.ApiManager.Manager.Dtos;

namespace Cloud.ApiManager.Manager
{
    public interface IManagerAppService : IApplicationService
    {
        GetOutput Get(GetInput input);

        Task Post(PostInput input);

        Task Put(PutInput input);

        Task Delete(DeleteInput input);

    }
}