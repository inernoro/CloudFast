using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstDocument.Dtos;

namespace Cloud.Temp.ConstDocument
{
    public class ConstDocumentAppService : CloudAppServiceBase, IConstDocumentAppService
    {
        private readonly IConstDocumentRepositories _constDocumentRepositories;
        public ConstDocumentAppService(IConstDocumentRepositories constDocumentRepositories)
        {
            _constDocumentRepositories = constDocumentRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstDocument>();
            return _constDocumentRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constDocumentRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constDocumentRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constDocumentRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constDocumentRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constDocumentRepositories.ToPaging("ConstDocument", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstDocumentDto>>() };
        }
    }
}
                    