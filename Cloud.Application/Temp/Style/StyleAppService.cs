using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Style.Dtos;

namespace Cloud.Temp.Style
{
    public class StyleAppService : CloudAppServiceBase, IStyleAppService
    {
        private readonly IStyleRepositories _styleRepositories;
        public StyleAppService(IStyleRepositories styleRepositories)
        {
            _styleRepositories = styleRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Style>();
            return _styleRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _styleRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _styleRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _styleRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _styleRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _styleRepositories.ToPaging("Style", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<StyleDto>>() };
        }
    }
}
                    