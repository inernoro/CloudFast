using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Tender.Dtos;
namespace Cloud.Tender
{
    public class TenderAppService : CloudAppServiceBase, ITenderAppService
    {
        private readonly ITenderRepositories _TenderRepositories;
        public TenderAppService(ITenderRepositories TenderRepositories)
        {
            _TenderRepositories = TenderRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Tender>();
            return _TenderRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _TenderRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _TenderRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _TenderRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _TenderRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _TenderRepositories.ToPaging("Tender", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TenderDto>>() };
        }
    }
}
                    