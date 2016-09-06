using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.HouseType.Dtos;
namespace Cloud.HouseType
{
    public class HouseTypeAppService : CloudAppServiceBase, IHouseTypeAppService
    {
        private readonly IHouseTypeRepositories _HouseTypeRepositories;
        public HouseTypeAppService(IHouseTypeRepositories HouseTypeRepositories)
        {
            _HouseTypeRepositories = HouseTypeRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.HouseType>();
            return _HouseTypeRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _HouseTypeRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _HouseTypeRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _HouseTypeRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _HouseTypeRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _HouseTypeRepositories.ToPaging("HouseType", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<HouseTypeDto>>() };
        }
    }
}
                    