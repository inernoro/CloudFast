using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Role.Dtos;
namespace Cloud.Role
{
    public class RoleAppService : CloudAppServiceBase, IRoleAppService
    {
        private readonly IRoleRepositories _RoleRepositories;
        public RoleAppService(IRoleRepositories RoleRepositories)
        {
            _RoleRepositories = RoleRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Role>();
            return _RoleRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _RoleRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _RoleRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _RoleRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _RoleRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _RoleRepositories.ToPaging("Role", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<RoleDto>>() };
        }
    }
}
                    