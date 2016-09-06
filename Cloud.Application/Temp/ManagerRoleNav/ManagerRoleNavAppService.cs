using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ManagerRoleNav.Dtos;

namespace Cloud.Temp.ManagerRoleNav
{
    public class ManagerRoleNavAppService : CloudAppServiceBase, IManagerRoleNavAppService
    {
        private readonly IManagerRoleNavRepositories _managerRoleNavRepositories;
        public ManagerRoleNavAppService(IManagerRoleNavRepositories managerRoleNavRepositories)
        {
            _managerRoleNavRepositories = managerRoleNavRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ManagerRoleNav>();
            return _managerRoleNavRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _managerRoleNavRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _managerRoleNavRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _managerRoleNavRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _managerRoleNavRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _managerRoleNavRepositories.ToPaging("ManagerRoleNav", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ManagerRoleNavDto>>() };
        }
    }
}
                    