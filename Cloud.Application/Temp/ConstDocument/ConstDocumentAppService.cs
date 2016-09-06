using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstDocument.Dtos;
namespace Cloud.ConstDocument
{
    public class ConstDocumentAppService : CloudAppServiceBase, IConstDocumentAppService
    {
        private readonly IConstDocumentRepositories _ConstDocumentRepositories;
        public ConstDocumentAppService(IConstDocumentRepositories ConstDocumentRepositories)
        {
            _ConstDocumentRepositories = ConstDocumentRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstDocument>();
            return _ConstDocumentRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstDocumentRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstDocumentRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstDocumentRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstDocumentRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstDocumentRepositories.ToPaging("ConstDocument", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstDocumentDto>>() };
        }
    }
}
                    