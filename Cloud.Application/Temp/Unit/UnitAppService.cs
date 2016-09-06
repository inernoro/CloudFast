using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Unit.Dtos;
namespace Cloud.Unit
{
    public class UnitAppService : CloudAppServiceBase, IUnitAppService
    {
        private readonly IUnitRepositories _UnitRepositories;
        public UnitAppService(IUnitRepositories UnitRepositories)
        {
            _UnitRepositories = UnitRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Unit>();
            return _UnitRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _UnitRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _UnitRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _UnitRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _UnitRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _UnitRepositories.ToPaging("Unit", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<UnitDto>>() };
        }
    }
}
                    