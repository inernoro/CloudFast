using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Shop.Dtos;

namespace Cloud.Temp.Shop
{
    public class ShopAppService : CloudAppServiceBase, IShopAppService
    {
        private readonly IShopRepositories _shopRepositories;
        public ShopAppService(IShopRepositories shopRepositories)
        {
            _shopRepositories = shopRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Shop>();
            return _shopRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _shopRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _shopRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _shopRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _shopRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _shopRepositories.ToPaging("Shop", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ShopDto>>() };
        }
    }
}
                    