using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.LogoType.Dtos;

namespace Cloud.Temp.LogoType
{
    public class LogoTypeAppService : CloudAppServiceBase, ILogoTypeAppService
    {
        private readonly ILogoTypeRepositories _logoTypeRepositories;
        public LogoTypeAppService(ILogoTypeRepositories logoTypeRepositories)
        {
            _logoTypeRepositories = logoTypeRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.LogoType>();
            return _logoTypeRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _logoTypeRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _logoTypeRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _logoTypeRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _logoTypeRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _logoTypeRepositories.ToPaging("LogoType", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<LogoTypeDto>>() };
        }
    }
}
                    