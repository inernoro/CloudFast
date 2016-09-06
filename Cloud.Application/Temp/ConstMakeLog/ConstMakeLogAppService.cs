using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstMakeLog.Dtos;

namespace Cloud.Temp.ConstMakeLog
{
    public class ConstMakeLogAppService : CloudAppServiceBase, IConstMakeLogAppService
    {
        private readonly IConstMakeLogRepositories _constMakeLogRepositories;
        public ConstMakeLogAppService(IConstMakeLogRepositories constMakeLogRepositories)
        {
            _constMakeLogRepositories = constMakeLogRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMakeLog>();
            return _constMakeLogRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constMakeLogRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constMakeLogRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constMakeLogRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constMakeLogRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constMakeLogRepositories.ToPaging("ConstMakeLog", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMakeLogDto>>() };
        }
    }
}
                    