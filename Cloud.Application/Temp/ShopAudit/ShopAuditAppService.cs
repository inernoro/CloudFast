using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ShopAudit.Dtos;
namespace Cloud.ShopAudit
{
    public class ShopAuditAppService : CloudAppServiceBase, IShopAuditAppService
    {
        private readonly IShopAuditRepositories _ShopAuditRepositories;
        public ShopAuditAppService(IShopAuditRepositories ShopAuditRepositories)
        {
            _ShopAuditRepositories = ShopAuditRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ShopAudit>();
            return _ShopAuditRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ShopAuditRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ShopAuditRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ShopAuditRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ShopAuditRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ShopAuditRepositories.ToPaging("ShopAudit", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ShopAuditDto>>() };
        }
    }
}
                    