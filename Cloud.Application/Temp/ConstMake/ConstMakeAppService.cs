using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstMake.Dtos;

namespace Cloud.Temp.ConstMake
{
    public class ConstMakeAppService : CloudAppServiceBase, IConstMakeAppService
    {
        private readonly IConstMakeRepositories _constMakeRepositories;
        public ConstMakeAppService(IConstMakeRepositories constMakeRepositories)
        {
            _constMakeRepositories = constMakeRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMake>();
            return _constMakeRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constMakeRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constMakeRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constMakeRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constMakeRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constMakeRepositories.ToPaging("ConstMake", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMakeDto>>() };
        }
    }
}
                    