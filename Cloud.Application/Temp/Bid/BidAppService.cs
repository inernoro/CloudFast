using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Bid.Dtos;
namespace Cloud.Bid
{
    public class BidAppService : CloudAppServiceBase, IBidAppService
    {
        private readonly IBidRepositories _BidRepositories;
        public BidAppService(IBidRepositories BidRepositories)
        {
            _BidRepositories = BidRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Bid>();
            return _BidRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _BidRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _BidRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _BidRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _BidRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _BidRepositories.ToPaging("Bid", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<BidDto>>() };
        }
    }
}
                    