using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.UnitCategory.Dtos;

namespace Cloud.Temp.UnitCategory
{
    public class UnitCategoryAppService : CloudAppServiceBase, IUnitCategoryAppService
    {
        private readonly IUnitCategoryRepositories _unitCategoryRepositories;
        public UnitCategoryAppService(IUnitCategoryRepositories unitCategoryRepositories)
        {
            _unitCategoryRepositories = unitCategoryRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.UnitCategory>();
            return _unitCategoryRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _unitCategoryRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _unitCategoryRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _unitCategoryRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _unitCategoryRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _unitCategoryRepositories.ToPaging("UnitCategory", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UnitCategoryDto>>() };
        }
    }
}
                    