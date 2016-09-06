using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.PlanDesignRenderDetail.Dtos;
namespace Cloud.PlanDesignRenderDetail
{
    public class PlanDesignRenderDetailAppService : CloudAppServiceBase, IPlanDesignRenderDetailAppService
    {
        private readonly IPlanDesignRenderDetailRepositories _PlanDesignRenderDetailRepositories;
        public PlanDesignRenderDetailAppService(IPlanDesignRenderDetailRepositories PlanDesignRenderDetailRepositories)
        {
            _PlanDesignRenderDetailRepositories = PlanDesignRenderDetailRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.PlanDesignRenderDetail>();
            return _PlanDesignRenderDetailRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _PlanDesignRenderDetailRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _PlanDesignRenderDetailRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _PlanDesignRenderDetailRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _PlanDesignRenderDetailRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _PlanDesignRenderDetailRepositories.ToPaging("PlanDesignRenderDetail", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<PlanDesignRenderDetailDto>>() };
        }
    }
}
                    