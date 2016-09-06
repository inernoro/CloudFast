using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Case.Dtos;

namespace Cloud.Temp.Case
{
    public class CaseAppService : CloudAppServiceBase, ICaseAppService
    {
        private readonly ICaseRepositories _caseRepositories;
        public CaseAppService(ICaseRepositories caseRepositories)
        {
            _caseRepositories = caseRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Case>();
            return _caseRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _caseRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _caseRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _caseRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _caseRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _caseRepositories.ToPaging("Case", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<CaseDto>>() };
        }
    }
}
                    