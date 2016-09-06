using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.sysdiagrams.Dtos;

namespace Cloud.Temp.sysdiagrams
{
    public class SysdiagramsAppService : CloudAppServiceBase, ISysdiagramsAppService
    {
        private readonly IsysdiagramsRepositories _sysdiagramsRepositories;
        public SysdiagramsAppService(IsysdiagramsRepositories sysdiagramsRepositories)
        {
            _sysdiagramsRepositories = sysdiagramsRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.sysdiagrams>();
            return _sysdiagramsRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _sysdiagramsRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _sysdiagramsRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _sysdiagramsRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _sysdiagramsRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _sysdiagramsRepositories.ToPaging("sysdiagrams", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<SysdiagramsDto>>() };
        }
    }
}
                    