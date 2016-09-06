using System.Threading.Tasks;
using Abp.Application.Services;
using Cloud.Framework.Assembly;
using Cloud.ChatMessage.Dtos;
namespace Cloud.ChatMessage
{
    public interface IChatMessageAppService : IApplicationService
    {
        [ContentDisplay("添加")]
        Task Post(PostInput input);
        [ContentDisplay("删除")]
        Task Delete(DeletetInput input);
        [ContentDisplay("修改")]
        Task Put(PutInput input);
        [ContentDisplay("获取")]
        Task<GetOutput> Get(GetInput input);
        [ContentDisplay("获取多条")]
        Task<GetAllOutput> GetAll(GetAllInput input);
    }
}