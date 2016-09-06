using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Team.Dtos;
namespace Cloud.Team
{
    public class TeamAppService : CloudAppServiceBase, ITeamAppService
    {
        private readonly ITeamRepositories _TeamRepositories;
        public TeamAppService(ITeamRepositories TeamRepositories)
        {
            _TeamRepositories = TeamRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Team>();
            return _TeamRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _TeamRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _TeamRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _TeamRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _TeamRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _TeamRepositories.ToPaging("Team", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TeamDto>>() };
        }
    }
}
                    