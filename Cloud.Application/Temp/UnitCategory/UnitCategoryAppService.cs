using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.UnitCategory.Dtos;
namespace Cloud.UnitCategory
{
    public class UnitCategoryAppService : CloudAppServiceBase, IUnitCategoryAppService
    {
        private readonly IUnitCategoryRepositories _UnitCategoryRepositories;
        public UnitCategoryAppService(IUnitCategoryRepositories UnitCategoryRepositories)
        {
            _UnitCategoryRepositories = UnitCategoryRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.UnitCategory>();
            return _UnitCategoryRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _UnitCategoryRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _UnitCategoryRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _UnitCategoryRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _UnitCategoryRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _UnitCategoryRepositories.ToPaging("UnitCategory", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UnitCategoryDto>>() };
        }
    }
}
                    