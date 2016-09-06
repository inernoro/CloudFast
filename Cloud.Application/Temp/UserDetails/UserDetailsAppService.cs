using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.UserDetails.Dtos;

namespace Cloud.Temp.UserDetails
{
    public class UserDetailsAppService : CloudAppServiceBase, IUserDetailsAppService
    {
        private readonly IUserDetailsRepositories _userDetailsRepositories;
        public UserDetailsAppService(IUserDetailsRepositories userDetailsRepositories)
        {
            _userDetailsRepositories = userDetailsRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.UserDetails>();
            return _userDetailsRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _userDetailsRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _userDetailsRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _userDetailsRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _userDetailsRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _userDetailsRepositories.ToPaging("UserDetails", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UserDetailsDto>>() };
        }
    }
}
                    