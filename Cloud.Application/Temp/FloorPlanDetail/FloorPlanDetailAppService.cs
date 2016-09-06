using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.FloorPlanDetail.Dtos;
namespace Cloud.FloorPlanDetail
{
    public class FloorPlanDetailAppService : CloudAppServiceBase, IFloorPlanDetailAppService
    {
        private readonly IFloorPlanDetailRepositories _FloorPlanDetailRepositories;
        public FloorPlanDetailAppService(IFloorPlanDetailRepositories FloorPlanDetailRepositories)
        {
            _FloorPlanDetailRepositories = FloorPlanDetailRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.FloorPlanDetail>();
            return _FloorPlanDetailRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _FloorPlanDetailRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _FloorPlanDetailRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _FloorPlanDetailRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _FloorPlanDetailRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _FloorPlanDetailRepositories.ToPaging("FloorPlanDetail", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<FloorPlanDetailDto>>() };
        }
    }
}
                    