using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Bid.Dtos;

namespace Cloud.Temp.Bid
{
    public class BidAppService : CloudAppServiceBase, IBidAppService
    {
        private readonly IBidRepositories _bidRepositories;
        public BidAppService(IBidRepositories bidRepositories)
        {
            _bidRepositories = bidRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Bid>();
            return _bidRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _bidRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _bidRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _bidRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _bidRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _bidRepositories.ToPaging("Bid", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<BidDto>>() };
        }
    }
}
                    