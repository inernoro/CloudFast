using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ProductProtection.Dtos;
namespace Cloud.ProductProtection
{
    public class ProductProtectionAppService : CloudAppServiceBase, IProductProtectionAppService
    {
        private readonly IProductProtectionRepositories _ProductProtectionRepositories;
        public ProductProtectionAppService(IProductProtectionRepositories ProductProtectionRepositories)
        {
            _ProductProtectionRepositories = ProductProtectionRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ProductProtection>();
            return _ProductProtectionRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ProductProtectionRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ProductProtectionRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ProductProtectionRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ProductProtectionRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ProductProtectionRepositories.ToPaging("ProductProtection", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ProductProtectionDto>>() };
        }
    }
}
                    