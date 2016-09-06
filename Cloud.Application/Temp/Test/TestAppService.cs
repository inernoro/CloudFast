using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Test.Dtos;

namespace Cloud.Temp.Test
{
    public class TestAppService : CloudAppServiceBase, ITestAppService
    {
        private readonly ITestRepositories _testRepositories;
        public TestAppService(ITestRepositories testRepositories)
        {
            _testRepositories = testRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Test>();
            return _testRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _testRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _testRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _testRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _testRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _testRepositories.ToPaging("Test", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TestDto>>() };
        }
    }
}
                    