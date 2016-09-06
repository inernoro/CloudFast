using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstApply.Dtos;

namespace Cloud.Temp.ConstApply
{
    public class ConstApplyAppService : CloudAppServiceBase, IConstApplyAppService
    {
        private readonly IConstApplyRepositories _constApplyRepositories;
        public ConstApplyAppService(IConstApplyRepositories constApplyRepositories)
        {
            _constApplyRepositories = constApplyRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstApply>();
            return _constApplyRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constApplyRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constApplyRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constApplyRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constApplyRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constApplyRepositories.ToPaging("ConstApply", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstApplyDto>>() };
        }
    }
}
                    