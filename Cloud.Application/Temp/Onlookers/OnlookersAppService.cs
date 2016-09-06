using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Onlookers.Dtos;

namespace Cloud.Temp.Onlookers
{
    public class OnlookersAppService : CloudAppServiceBase, IOnlookersAppService
    {
        private readonly IOnlookersRepositories _onlookersRepositories;
        public OnlookersAppService(IOnlookersRepositories onlookersRepositories)
        {
            _onlookersRepositories = onlookersRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Onlookers>();
            return _onlookersRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _onlookersRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _onlookersRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _onlookersRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _onlookersRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _onlookersRepositories.ToPaging("Onlookers", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<OnlookersDto>>() };
        }
    }
}
                    