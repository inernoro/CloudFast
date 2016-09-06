using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstructLog.Dtos;
namespace Cloud.ConstructLog
{
    public class ConstructLogAppService : CloudAppServiceBase, IConstructLogAppService
    {
        private readonly IConstructLogRepositories _ConstructLogRepositories;
        public ConstructLogAppService(IConstructLogRepositories ConstructLogRepositories)
        {
            _ConstructLogRepositories = ConstructLogRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstructLog>();
            return _ConstructLogRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstructLogRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstructLogRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstructLogRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstructLogRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstructLogRepositories.ToPaging("ConstructLog", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstructLogDto>>() };
        }
    }
}
                    