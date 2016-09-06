using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstApply.Dtos;
namespace Cloud.ConstApply
{
    public class ConstApplyAppService : CloudAppServiceBase, IConstApplyAppService
    {
        private readonly IConstApplyRepositories _ConstApplyRepositories;
        public ConstApplyAppService(IConstApplyRepositories ConstApplyRepositories)
        {
            _ConstApplyRepositories = ConstApplyRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstApply>();
            return _ConstApplyRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstApplyRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstApplyRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstApplyRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstApplyRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstApplyRepositories.ToPaging("ConstApply", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstApplyDto>>() };
        }
    }
}
                    