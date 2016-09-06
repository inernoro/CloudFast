using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Construct.Dtos;
namespace Cloud.Construct
{
    public class ConstructAppService : CloudAppServiceBase, IConstructAppService
    {
        private readonly IConstructRepositories _ConstructRepositories;
        public ConstructAppService(IConstructRepositories ConstructRepositories)
        {
            _ConstructRepositories = ConstructRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Construct>();
            return _ConstructRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstructRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstructRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstructRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstructRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstructRepositories.ToPaging("Construct", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstructDto>>() };
        }
    }
}
                    