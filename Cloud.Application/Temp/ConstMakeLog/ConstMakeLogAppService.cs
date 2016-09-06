using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstMakeLog.Dtos;
namespace Cloud.ConstMakeLog
{
    public class ConstMakeLogAppService : CloudAppServiceBase, IConstMakeLogAppService
    {
        private readonly IConstMakeLogRepositories _ConstMakeLogRepositories;
        public ConstMakeLogAppService(IConstMakeLogRepositories ConstMakeLogRepositories)
        {
            _ConstMakeLogRepositories = ConstMakeLogRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMakeLog>();
            return _ConstMakeLogRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstMakeLogRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstMakeLogRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstMakeLogRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstMakeLogRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstMakeLogRepositories.ToPaging("ConstMakeLog", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMakeLogDto>>() };
        }
    }
}
                    