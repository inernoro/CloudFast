using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstructionShop.Dtos;
namespace Cloud.ConstructionShop
{
    public class ConstructionShopAppService : CloudAppServiceBase, IConstructionShopAppService
    {
        private readonly IConstructionShopRepositories _ConstructionShopRepositories;
        public ConstructionShopAppService(IConstructionShopRepositories ConstructionShopRepositories)
        {
            _ConstructionShopRepositories = ConstructionShopRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstructionShop>();
            return _ConstructionShopRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstructionShopRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstructionShopRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstructionShopRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstructionShopRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstructionShopRepositories.ToPaging("ConstructionShop", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstructionShopDto>>() };
        }
    }
}
                    