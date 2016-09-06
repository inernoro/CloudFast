using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ProductProtection.Dtos;

namespace Cloud.Temp.ProductProtection
{
    public class ProductProtectionAppService : CloudAppServiceBase, IProductProtectionAppService
    {
        private readonly IProductProtectionRepositories _productProtectionRepositories;
        public ProductProtectionAppService(IProductProtectionRepositories productProtectionRepositories)
        {
            _productProtectionRepositories = productProtectionRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ProductProtection>();
            return _productProtectionRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _productProtectionRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _productProtectionRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _productProtectionRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _productProtectionRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _productProtectionRepositories.ToPaging("ProductProtection", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ProductProtectionDto>>() };
        }
    }
}
                    