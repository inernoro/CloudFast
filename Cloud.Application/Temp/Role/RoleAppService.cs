using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Role.Dtos;

namespace Cloud.Temp.Role
{
    public class RoleAppService : CloudAppServiceBase, IRoleAppService
    {
        private readonly IRoleRepositories _roleRepositories;
        public RoleAppService(IRoleRepositories roleRepositories)
        {
            _roleRepositories = roleRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Role>();
            return _roleRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _roleRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _roleRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _roleRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _roleRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _roleRepositories.ToPaging("Role", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<RoleDto>>() };
        }
    }
}
                    