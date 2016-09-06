using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstAcceptance.Dtos;

namespace Cloud.Temp.ConstAcceptance
{
    public class ConstAcceptanceAppService : CloudAppServiceBase, IConstAcceptanceAppService
    {
        private readonly IConstAcceptanceRepositories _constAcceptanceRepositories;
        public ConstAcceptanceAppService(IConstAcceptanceRepositories constAcceptanceRepositories)
        {
            _constAcceptanceRepositories = constAcceptanceRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstAcceptance>();
            return _constAcceptanceRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constAcceptanceRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constAcceptanceRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constAcceptanceRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constAcceptanceRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constAcceptanceRepositories.ToPaging("ConstAcceptance", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstAcceptanceDto>>() };
        }
    }
}
                    