using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Team.Dtos;

namespace Cloud.Temp.Team
{
    public class TeamAppService : CloudAppServiceBase, ITeamAppService
    {
        private readonly ITeamRepositories _teamRepositories;
        public TeamAppService(ITeamRepositories teamRepositories)
        {
            _teamRepositories = teamRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Team>();
            return _teamRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _teamRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _teamRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _teamRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _teamRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _teamRepositories.ToPaging("Team", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TeamDto>>() };
        }
    }
}
                    