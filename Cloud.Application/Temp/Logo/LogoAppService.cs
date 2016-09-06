using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Logo.Dtos;
namespace Cloud.Logo
{
    public class LogoAppService : CloudAppServiceBase, ILogoAppService
    {
        private readonly ILogoRepositories _LogoRepositories;
        public LogoAppService(ILogoRepositories LogoRepositories)
        {
            _LogoRepositories = LogoRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Logo>();
            return _LogoRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _LogoRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _LogoRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _LogoRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _LogoRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _LogoRepositories.ToPaging("Logo", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<LogoDto>>() };
        }
    }
}
                    