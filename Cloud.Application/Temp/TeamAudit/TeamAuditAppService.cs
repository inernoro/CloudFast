using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.TeamAudit.Dtos;

namespace Cloud.Temp.TeamAudit
{
    public class TeamAuditAppService : CloudAppServiceBase, ITeamAuditAppService
    {
        private readonly ITeamAuditRepositories _teamAuditRepositories;
        public TeamAuditAppService(ITeamAuditRepositories teamAuditRepositories)
        {
            _teamAuditRepositories = teamAuditRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.TeamAudit>();
            return _teamAuditRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _teamAuditRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _teamAuditRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _teamAuditRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _teamAuditRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _teamAuditRepositories.ToPaging("TeamAudit", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TeamAuditDto>>() };
        }
    }
}
                    