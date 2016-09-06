using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Onlookers.Dtos;
namespace Cloud.Onlookers
{
    public class OnlookersAppService : CloudAppServiceBase, IOnlookersAppService
    {
        private readonly IOnlookersRepositories _OnlookersRepositories;
        public OnlookersAppService(IOnlookersRepositories OnlookersRepositories)
        {
            _OnlookersRepositories = OnlookersRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Onlookers>();
            return _OnlookersRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _OnlookersRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _OnlookersRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _OnlookersRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _OnlookersRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _OnlookersRepositories.ToPaging("Onlookers", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<OnlookersDto>>() };
        }
    }
}
                    