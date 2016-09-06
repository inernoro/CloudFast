using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.TeamAudit.Dtos;
namespace Cloud.TeamAudit
{
    public class TeamAuditAppService : CloudAppServiceBase, ITeamAuditAppService
    {
        private readonly ITeamAuditRepositories _TeamAuditRepositories;
        public TeamAuditAppService(ITeamAuditRepositories TeamAuditRepositories)
        {
            _TeamAuditRepositories = TeamAuditRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.TeamAudit>();
            return _TeamAuditRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _TeamAuditRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _TeamAuditRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _TeamAuditRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _TeamAuditRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _TeamAuditRepositories.ToPaging("TeamAudit", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TeamAuditDto>>() };
        }
    }
}
                    