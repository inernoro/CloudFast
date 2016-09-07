using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ScriptManager.ScriptManagerApp.Dtos;

namespace Cloud.ScriptManager.ScriptManagerApp
{
    public class ScriptManagerAppService : CloudAppServiceBase, IScriptManagerAppService
    {
        private readonly IScriptManagerRepositories _scriptManagerRepositories;
        public ScriptManagerAppService(IScriptManagerRepositories scriptManagerRepositories)
        {
            _scriptManagerRepositories = scriptManagerRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ScriptManager>();
            return _scriptManagerRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _scriptManagerRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _scriptManagerRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _scriptManagerRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _scriptManagerRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _scriptManagerRepositories.ToPaging("ScriptManager", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ScriptManagerDto>>() };
        }
    }
}
                    