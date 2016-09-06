using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstContract.Dtos;

namespace Cloud.Temp.ConstContract
{
    public class ConstContractAppService : CloudAppServiceBase, IConstContractAppService
    {
        private readonly IConstContractRepositories _constContractRepositories;
        public ConstContractAppService(IConstContractRepositories constContractRepositories)
        {
            _constContractRepositories = constContractRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstContract>();
            return _constContractRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constContractRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constContractRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constContractRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constContractRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constContractRepositories.ToPaging("ConstContract", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstContractDto>>() };
        }
    }
}
                    