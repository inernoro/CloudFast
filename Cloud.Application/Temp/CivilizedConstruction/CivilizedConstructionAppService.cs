using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.CivilizedConstruction.Dtos;

namespace Cloud.Temp.CivilizedConstruction
{
    public class CivilizedConstructionAppService : CloudAppServiceBase, ICivilizedConstructionAppService
    {
        private readonly ICivilizedConstructionRepositories _civilizedConstructionRepositories;
        public CivilizedConstructionAppService(ICivilizedConstructionRepositories civilizedConstructionRepositories)
        {
            _civilizedConstructionRepositories = civilizedConstructionRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.CivilizedConstruction>();
            return _civilizedConstructionRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _civilizedConstructionRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _civilizedConstructionRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _civilizedConstructionRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _civilizedConstructionRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _civilizedConstructionRepositories.ToPaging("CivilizedConstruction", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<CivilizedConstructionDto>>() };
        }
    }
}
                    