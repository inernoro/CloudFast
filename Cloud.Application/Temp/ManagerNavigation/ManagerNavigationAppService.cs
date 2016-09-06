using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ManagerNavigation.Dtos;
namespace Cloud.ManagerNavigation
{
    public class ManagerNavigationAppService : CloudAppServiceBase, IManagerNavigationAppService
    {
        private readonly IManagerNavigationRepositories _ManagerNavigationRepositories;
        public ManagerNavigationAppService(IManagerNavigationRepositories ManagerNavigationRepositories)
        {
            _ManagerNavigationRepositories = ManagerNavigationRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ManagerNavigation>();
            return _ManagerNavigationRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ManagerNavigationRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ManagerNavigationRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ManagerNavigationRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ManagerNavigationRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ManagerNavigationRepositories.ToPaging("ManagerNavigation", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ManagerNavigationDto>>() };
        }
    }
}
                    