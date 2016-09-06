using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstructLog.Dtos;

namespace Cloud.Temp.ConstructLog
{
    public class ConstructLogAppService : CloudAppServiceBase, IConstructLogAppService
    {
        private readonly IConstructLogRepositories _constructLogRepositories;
        public ConstructLogAppService(IConstructLogRepositories constructLogRepositories)
        {
            _constructLogRepositories = constructLogRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstructLog>();
            return _constructLogRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constructLogRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constructLogRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constructLogRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constructLogRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constructLogRepositories.ToPaging("ConstructLog", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstructLogDto>>() };
        }
    }
}
                    