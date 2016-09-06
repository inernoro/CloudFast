using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ShopAudit.Dtos;

namespace Cloud.Temp.ShopAudit
{
    public class ShopAuditAppService : CloudAppServiceBase, IShopAuditAppService
    {
        private readonly IShopAuditRepositories _shopAuditRepositories;
        public ShopAuditAppService(IShopAuditRepositories shopAuditRepositories)
        {
            _shopAuditRepositories = shopAuditRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ShopAudit>();
            return _shopAuditRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _shopAuditRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _shopAuditRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _shopAuditRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _shopAuditRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _shopAuditRepositories.ToPaging("ShopAudit", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ShopAuditDto>>() };
        }
    }
}
                    