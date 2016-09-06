using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.UserDetails.Dtos;
namespace Cloud.UserDetails
{
    public class UserDetailsAppService : CloudAppServiceBase, IUserDetailsAppService
    {
        private readonly IUserDetailsRepositories _UserDetailsRepositories;
        public UserDetailsAppService(IUserDetailsRepositories UserDetailsRepositories)
        {
            _UserDetailsRepositories = UserDetailsRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.UserDetails>();
            return _UserDetailsRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _UserDetailsRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _UserDetailsRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _UserDetailsRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _UserDetailsRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _UserDetailsRepositories.ToPaging("UserDetails", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UserDetailsDto>>() };
        }
    }
}
                    