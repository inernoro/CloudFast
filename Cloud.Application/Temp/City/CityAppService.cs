using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.City.Dtos;

namespace Cloud.Temp.City
{
    public class CityAppService : CloudAppServiceBase, ICityAppService
    {
        private readonly ICityRepositories _cityRepositories;
        public CityAppService(ICityRepositories cityRepositories)
        {
            _cityRepositories = cityRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.City>();
            return _cityRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _cityRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _cityRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _cityRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _cityRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _cityRepositories.ToPaging("City", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<CityDto>>() };
        }
    }
}
                    