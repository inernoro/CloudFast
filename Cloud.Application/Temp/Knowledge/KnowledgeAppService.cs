using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Knowledge.Dtos;
namespace Cloud.Knowledge
{
    public class KnowledgeAppService : CloudAppServiceBase, IKnowledgeAppService
    {
        private readonly IKnowledgeRepositories _KnowledgeRepositories;
        public KnowledgeAppService(IKnowledgeRepositories KnowledgeRepositories)
        {
            _KnowledgeRepositories = KnowledgeRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Knowledge>();
            return _KnowledgeRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _KnowledgeRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _KnowledgeRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _KnowledgeRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _KnowledgeRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _KnowledgeRepositories.ToPaging("Knowledge", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<KnowledgeDto>>() };
        }
    }
}
                    