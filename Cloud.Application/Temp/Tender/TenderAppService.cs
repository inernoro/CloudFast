using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Tender.Dtos;

namespace Cloud.Temp.Tender
{
    public class TenderAppService : CloudAppServiceBase, ITenderAppService
    {
        private readonly ITenderRepositories _tenderRepositories;
        public TenderAppService(ITenderRepositories tenderRepositories)
        {
            _tenderRepositories = tenderRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Tender>();
            return _tenderRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _tenderRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _tenderRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _tenderRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _tenderRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _tenderRepositories.ToPaging("Tender", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TenderDto>>() };
        }
    }
}
                    