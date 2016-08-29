using System.Threading.Tasks;
using Abp;
using Cloud.ApiManager.Manager.Dtos;

namespace Cloud.ApiManager.Manager
{
    public class ManagerAppService : AbpServiceBase, IManagerAppService
    { 

        public GetOutput Get(GetInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task Post(PostInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task Put(PutInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(DeleteInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}