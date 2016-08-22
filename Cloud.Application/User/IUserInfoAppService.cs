using System.Web.Http;
using Abp.Application.Services;

namespace Cloud.User
{
    public interface IUserInfoAppService : IApplicationService
    {

        [HttpGet]
        void Get(); 

    }
}