using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.UserInfo.Dtos;

namespace Cloud.Temp.UserInfo
{
    public class UserInfoAppService : CloudAppServiceBase, IUserInfoAppService
    {
        private readonly IUserInfoRepositories _userInfoRepositories;
        public UserInfoAppService(IUserInfoRepositories userInfoRepositories)
        {
            _userInfoRepositories = userInfoRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.UserInfo>();
            return _userInfoRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _userInfoRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _userInfoRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _userInfoRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _userInfoRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _userInfoRepositories.ToPaging("UserInfo", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UserInfoDto>>() };
        }
    }
}
                    