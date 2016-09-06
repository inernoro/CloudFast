using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstMessage.Dtos;
namespace Cloud.ConstMessage
{
    public class ConstMessageAppService : CloudAppServiceBase, IConstMessageAppService
    {
        private readonly IConstMessageRepositories _ConstMessageRepositories;
        public ConstMessageAppService(IConstMessageRepositories ConstMessageRepositories)
        {
            _ConstMessageRepositories = ConstMessageRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMessage>();
            return _ConstMessageRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstMessageRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstMessageRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstMessageRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstMessageRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstMessageRepositories.ToPaging("ConstMessage", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMessageDto>>() };
        }
    }
}
                    