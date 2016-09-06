using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Unit.Dtos;

namespace Cloud.Temp.Unit
{
    public class UnitAppService : CloudAppServiceBase, IUnitAppService
    {
        private readonly IUnitRepositories _unitRepositories;
        public UnitAppService(IUnitRepositories unitRepositories)
        {
            _unitRepositories = unitRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Unit>();
            return _unitRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _unitRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _unitRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _unitRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _unitRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _unitRepositories.ToPaging("Unit", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UnitDto>>() };
        }
    }
}
                    