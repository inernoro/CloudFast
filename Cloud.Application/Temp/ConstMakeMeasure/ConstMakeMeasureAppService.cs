using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstMakeMeasure.Dtos;

namespace Cloud.Temp.ConstMakeMeasure
{
    public class ConstMakeMeasureAppService : CloudAppServiceBase, IConstMakeMeasureAppService
    {
        private readonly IConstMakeMeasureRepositories _constMakeMeasureRepositories;
        public ConstMakeMeasureAppService(IConstMakeMeasureRepositories constMakeMeasureRepositories)
        {
            _constMakeMeasureRepositories = constMakeMeasureRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMakeMeasure>();
            return _constMakeMeasureRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constMakeMeasureRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constMakeMeasureRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constMakeMeasureRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constMakeMeasureRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constMakeMeasureRepositories.ToPaging("ConstMakeMeasure", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMakeMeasureDto>>() };
        }
    }
}
                    