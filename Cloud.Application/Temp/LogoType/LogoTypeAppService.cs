using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.LogoType.Dtos;
namespace Cloud.LogoType
{
    public class LogoTypeAppService : CloudAppServiceBase, ILogoTypeAppService
    {
        private readonly ILogoTypeRepositories _LogoTypeRepositories;
        public LogoTypeAppService(ILogoTypeRepositories LogoTypeRepositories)
        {
            _LogoTypeRepositories = LogoTypeRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.LogoType>();
            return _LogoTypeRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _LogoTypeRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _LogoTypeRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _LogoTypeRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _LogoTypeRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _LogoTypeRepositories.ToPaging("LogoType", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<LogoTypeDto>>() };
        }
    }
}
                    