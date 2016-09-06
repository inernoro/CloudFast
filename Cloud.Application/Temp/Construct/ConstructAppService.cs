using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Construct.Dtos;

namespace Cloud.Temp.Construct
{
    public class ConstructAppService : CloudAppServiceBase, IConstructAppService
    {
        private readonly IConstructRepositories _constructRepositories;
        public ConstructAppService(IConstructRepositories constructRepositories)
        {
            _constructRepositories = constructRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Construct>();
            return _constructRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constructRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constructRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constructRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constructRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constructRepositories.ToPaging("Construct", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstructDto>>() };
        }
    }
}
                    