using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Test.Dtos;
namespace Cloud.Test
{
    public class TestAppService : CloudAppServiceBase, ITestAppService
    {
        private readonly ITestRepositories _TestRepositories;
        public TestAppService(ITestRepositories TestRepositories)
        {
            _TestRepositories = TestRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Test>();
            return _TestRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _TestRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _TestRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _TestRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _TestRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _TestRepositories.ToPaging("Test", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TestDto>>() };
        }
    }
}
                    