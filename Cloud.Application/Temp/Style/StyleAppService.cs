using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Style.Dtos;
namespace Cloud.Style
{
    public class StyleAppService : CloudAppServiceBase, IStyleAppService
    {
        private readonly IStyleRepositories _StyleRepositories;
        public StyleAppService(IStyleRepositories StyleRepositories)
        {
            _StyleRepositories = StyleRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Style>();
            return _StyleRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _StyleRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _StyleRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _StyleRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _StyleRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _StyleRepositories.ToPaging("Style", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<StyleDto>>() };
        }
    }
}
                    