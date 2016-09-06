using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ApplyJoinShop.Dtos;

namespace Cloud.Temp.ApplyJoinShop
{
    public class ApplyJoinShopAppService : CloudAppServiceBase, IApplyJoinShopAppService
    {
        private readonly IApplyJoinShopRepositories _applyJoinShopRepositories;
        public ApplyJoinShopAppService(IApplyJoinShopRepositories applyJoinShopRepositories)
        {
            _applyJoinShopRepositories = applyJoinShopRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ApplyJoinShop>();
            return _applyJoinShopRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _applyJoinShopRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _applyJoinShopRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _applyJoinShopRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _applyJoinShopRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _applyJoinShopRepositories.ToPaging("ApplyJoinShop", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ApplyJoinShopDto>>() };
        }
    }
}
                    