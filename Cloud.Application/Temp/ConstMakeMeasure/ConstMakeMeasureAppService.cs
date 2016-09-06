using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstMakeMeasure.Dtos;
namespace Cloud.ConstMakeMeasure
{
    public class ConstMakeMeasureAppService : CloudAppServiceBase, IConstMakeMeasureAppService
    {
        private readonly IConstMakeMeasureRepositories _ConstMakeMeasureRepositories;
        public ConstMakeMeasureAppService(IConstMakeMeasureRepositories ConstMakeMeasureRepositories)
        {
            _ConstMakeMeasureRepositories = ConstMakeMeasureRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMakeMeasure>();
            return _ConstMakeMeasureRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstMakeMeasureRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstMakeMeasureRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstMakeMeasureRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstMakeMeasureRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstMakeMeasureRepositories.ToPaging("ConstMakeMeasure", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMakeMeasureDto>>() };
        }
    }
}
                    