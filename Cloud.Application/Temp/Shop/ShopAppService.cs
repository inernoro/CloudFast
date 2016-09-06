using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Shop.Dtos;
namespace Cloud.Shop
{
    public class ShopAppService : CloudAppServiceBase, IShopAppService
    {
        private readonly IShopRepositories _ShopRepositories;
        public ShopAppService(IShopRepositories ShopRepositories)
        {
            _ShopRepositories = ShopRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Shop>();
            return _ShopRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ShopRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ShopRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ShopRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ShopRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ShopRepositories.ToPaging("Shop", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ShopDto>>() };
        }
    }
}
                    