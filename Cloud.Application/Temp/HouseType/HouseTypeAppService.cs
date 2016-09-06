using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.HouseType.Dtos;

namespace Cloud.Temp.HouseType
{
    public class HouseTypeAppService : CloudAppServiceBase, IHouseTypeAppService
    {
        private readonly IHouseTypeRepositories _houseTypeRepositories;
        public HouseTypeAppService(IHouseTypeRepositories houseTypeRepositories)
        {
            _houseTypeRepositories = houseTypeRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.HouseType>();
            return _houseTypeRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _houseTypeRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _houseTypeRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _houseTypeRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _houseTypeRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _houseTypeRepositories.ToPaging("HouseType", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<HouseTypeDto>>() };
        }
    }
}
                    