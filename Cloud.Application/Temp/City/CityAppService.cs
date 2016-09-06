using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.City.Dtos;
namespace Cloud.City
{
    public class CityAppService : CloudAppServiceBase, ICityAppService
    {
        private readonly ICityRepositories _CityRepositories;
        public CityAppService(ICityRepositories CityRepositories)
        {
            _CityRepositories = CityRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.City>();
            return _CityRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _CityRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _CityRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _CityRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _CityRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _CityRepositories.ToPaging("City", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<CityDto>>() };
        }
    }
}
                    