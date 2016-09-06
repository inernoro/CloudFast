using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.FloorPlanDetail.Dtos;

namespace Cloud.Temp.FloorPlanDetail
{
    public class FloorPlanDetailAppService : CloudAppServiceBase, IFloorPlanDetailAppService
    {
        private readonly IFloorPlanDetailRepositories _floorPlanDetailRepositories;
        public FloorPlanDetailAppService(IFloorPlanDetailRepositories floorPlanDetailRepositories)
        {
            _floorPlanDetailRepositories = floorPlanDetailRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.FloorPlanDetail>();
            return _floorPlanDetailRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _floorPlanDetailRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _floorPlanDetailRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _floorPlanDetailRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _floorPlanDetailRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _floorPlanDetailRepositories.ToPaging("FloorPlanDetail", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<FloorPlanDetailDto>>() };
        }
    }
}
                    