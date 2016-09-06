using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstAcceptance.Dtos;
namespace Cloud.ConstAcceptance
{
    public class ConstAcceptanceAppService : CloudAppServiceBase, IConstAcceptanceAppService
    {
        private readonly IConstAcceptanceRepositories _ConstAcceptanceRepositories;
        public ConstAcceptanceAppService(IConstAcceptanceRepositories ConstAcceptanceRepositories)
        {
            _ConstAcceptanceRepositories = ConstAcceptanceRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstAcceptance>();
            return _ConstAcceptanceRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstAcceptanceRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstAcceptanceRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstAcceptanceRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstAcceptanceRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstAcceptanceRepositories.ToPaging("ConstAcceptance", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstAcceptanceDto>>() };
        }
    }
}
                    