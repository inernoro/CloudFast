using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ApplyJoinShop.Dtos;
namespace Cloud.ApplyJoinShop
{
    public class ApplyJoinShopAppService : CloudAppServiceBase, IApplyJoinShopAppService
    {
        private readonly IApplyJoinShopRepositories _ApplyJoinShopRepositories;
        public ApplyJoinShopAppService(IApplyJoinShopRepositories ApplyJoinShopRepositories)
        {
            _ApplyJoinShopRepositories = ApplyJoinShopRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ApplyJoinShop>();
            return _ApplyJoinShopRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ApplyJoinShopRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ApplyJoinShopRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ApplyJoinShopRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ApplyJoinShopRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ApplyJoinShopRepositories.ToPaging("ApplyJoinShop", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ApplyJoinShopDto>>() };
        }
    }
}
                    