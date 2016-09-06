using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ChatMessage.Dtos;
namespace Cloud.ChatMessage
{
    public class ChatMessageAppService : CloudAppServiceBase, IChatMessageAppService
    {
        private readonly IChatMessageRepositories _ChatMessageRepositories;
        public ChatMessageAppService(IChatMessageRepositories ChatMessageRepositories)
        {
            _ChatMessageRepositories = ChatMessageRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ChatMessage>();
            return _ChatMessageRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ChatMessageRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ChatMessageRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ChatMessageRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ChatMessageRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ChatMessageRepositories.ToPaging("ChatMessage", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ChatMessageDto>>() };
        }
    }
}
                    