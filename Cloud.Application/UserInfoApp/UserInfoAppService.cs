using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp;
using Cloud.UserInfoApp.Dtos;
namespace Cloud.UserInfoApp
{
    public class UserInfoAppService : CloudAppServiceBase, IUserInfoAppService
    {
        private readonly IUserInfoRepositories _UserInfoRepositories;
        public UserInfoAppService(IUserInfoRepositories UserInfoRepositories)
        {
            _UserInfoRepositories = UserInfoRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<UserInfo>();
            return _UserInfoRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _UserInfoRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _UserInfoRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _UserInfoRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _UserInfoRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _UserInfoRepositories.ToPaging("UserInfo", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UserInfoDto>>() };
        }
    }
}
                    