using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.CivilizedConstruction.Dtos;
namespace Cloud.CivilizedConstruction
{
    public class CivilizedConstructionAppService : CloudAppServiceBase, ICivilizedConstructionAppService
    {
        private readonly ICivilizedConstructionRepositories _CivilizedConstructionRepositories;
        public CivilizedConstructionAppService(ICivilizedConstructionRepositories CivilizedConstructionRepositories)
        {
            _CivilizedConstructionRepositories = CivilizedConstructionRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.CivilizedConstruction>();
            return _CivilizedConstructionRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _CivilizedConstructionRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _CivilizedConstructionRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _CivilizedConstructionRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _CivilizedConstructionRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _CivilizedConstructionRepositories.ToPaging("CivilizedConstruction", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<CivilizedConstructionDto>>() };
        }
    }
}
                    