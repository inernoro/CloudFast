using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Logo.Dtos;

namespace Cloud.Temp.Logo
{
    public class LogoAppService : CloudAppServiceBase, ILogoAppService
    {
        private readonly ILogoRepositories _logoRepositories;
        public LogoAppService(ILogoRepositories logoRepositories)
        {
            _logoRepositories = logoRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Logo>();
            return _logoRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _logoRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _logoRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _logoRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _logoRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _logoRepositories.ToPaging("Logo", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<LogoDto>>() };
        }
    }
}
                    