using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ManagerNavigation.Dtos;

namespace Cloud.Temp.ManagerNavigation
{
    public class ManagerNavigationAppService : CloudAppServiceBase, IManagerNavigationAppService
    {
        private readonly IManagerNavigationRepositories _managerNavigationRepositories;
        public ManagerNavigationAppService(IManagerNavigationRepositories managerNavigationRepositories)
        {
            _managerNavigationRepositories = managerNavigationRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ManagerNavigation>();
            return _managerNavigationRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _managerNavigationRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _managerNavigationRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _managerNavigationRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _managerNavigationRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _managerNavigationRepositories.ToPaging("ManagerNavigation", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ManagerNavigationDto>>() };
        }
    }
}
                    