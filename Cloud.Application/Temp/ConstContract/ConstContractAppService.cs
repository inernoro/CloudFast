using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstContract.Dtos;
namespace Cloud.ConstContract
{
    public class ConstContractAppService : CloudAppServiceBase, IConstContractAppService
    {
        private readonly IConstContractRepositories _ConstContractRepositories;
        public ConstContractAppService(IConstContractRepositories ConstContractRepositories)
        {
            _ConstContractRepositories = ConstContractRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstContract>();
            return _ConstContractRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstContractRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstContractRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstContractRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstContractRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstContractRepositories.ToPaging("ConstContract", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstContractDto>>() };
        }
    }
}
                    