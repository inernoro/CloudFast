using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Knowledge.Dtos;

namespace Cloud.Temp.Knowledge
{
    public class KnowledgeAppService : CloudAppServiceBase, IKnowledgeAppService
    {
        private readonly IKnowledgeRepositories _knowledgeRepositories;
        public KnowledgeAppService(IKnowledgeRepositories knowledgeRepositories)
        {
            _knowledgeRepositories = knowledgeRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Knowledge>();
            return _knowledgeRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _knowledgeRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _knowledgeRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _knowledgeRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _knowledgeRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _knowledgeRepositories.ToPaging("Knowledge", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<KnowledgeDto>>() };
        }
    }
}
                    