using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Case.Dtos;
namespace Cloud.Case
{
    public class CaseAppService : CloudAppServiceBase, ICaseAppService
    {
        private readonly ICaseRepositories _CaseRepositories;
        public CaseAppService(ICaseRepositories CaseRepositories)
        {
            _CaseRepositories = CaseRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Case>();
            return _CaseRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _CaseRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _CaseRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _CaseRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _CaseRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _CaseRepositories.ToPaging("Case", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<CaseDto>>() };
        }
    }
}
                    