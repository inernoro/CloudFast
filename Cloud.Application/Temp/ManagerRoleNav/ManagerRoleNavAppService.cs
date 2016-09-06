using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ManagerRoleNav.Dtos;
namespace Cloud.ManagerRoleNav
{
    public class ManagerRoleNavAppService : CloudAppServiceBase, IManagerRoleNavAppService
    {
        private readonly IManagerRoleNavRepositories _ManagerRoleNavRepositories;
        public ManagerRoleNavAppService(IManagerRoleNavRepositories ManagerRoleNavRepositories)
        {
            _ManagerRoleNavRepositories = ManagerRoleNavRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ManagerRoleNav>();
            return _ManagerRoleNavRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ManagerRoleNavRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ManagerRoleNavRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ManagerRoleNavRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ManagerRoleNavRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ManagerRoleNavRepositories.ToPaging("ManagerRoleNav", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ManagerRoleNavDto>>() };
        }
    }
}
                    