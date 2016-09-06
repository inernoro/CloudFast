using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstMessage.Dtos;

namespace Cloud.Temp.ConstMessage
{
    public class ConstMessageAppService : CloudAppServiceBase, IConstMessageAppService
    {
        private readonly IConstMessageRepositories _constMessageRepositories;
        public ConstMessageAppService(IConstMessageRepositories constMessageRepositories)
        {
            _constMessageRepositories = constMessageRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMessage>();
            return _constMessageRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constMessageRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constMessageRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constMessageRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constMessageRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constMessageRepositories.ToPaging("ConstMessage", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMessageDto>>() };
        }
    }
}
                    