using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstructionShop.Dtos;

namespace Cloud.Temp.ConstructionShop
{
    public class ConstructionShopAppService : CloudAppServiceBase, IConstructionShopAppService
    {
        private readonly IConstructionShopRepositories _constructionShopRepositories;
        public ConstructionShopAppService(IConstructionShopRepositories constructionShopRepositories)
        {
            _constructionShopRepositories = constructionShopRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstructionShop>();
            return _constructionShopRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constructionShopRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constructionShopRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constructionShopRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constructionShopRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constructionShopRepositories.ToPaging("ConstructionShop", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstructionShopDto>>() };
        }
    }
}
                    