using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ChatMessage.Dtos;

namespace Cloud.Temp.ChatMessage
{
    public class ChatMessageAppService : CloudAppServiceBase, IChatMessageAppService
    {
        private readonly IChatMessageRepositories _chatMessageRepositories;
        public ChatMessageAppService(IChatMessageRepositories chatMessageRepositories)
        {
            _chatMessageRepositories = chatMessageRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ChatMessage>();
            return _chatMessageRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _chatMessageRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _chatMessageRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _chatMessageRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _chatMessageRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _chatMessageRepositories.ToPaging("ChatMessage", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ChatMessageDto>>() };
        }
    }
}
                    