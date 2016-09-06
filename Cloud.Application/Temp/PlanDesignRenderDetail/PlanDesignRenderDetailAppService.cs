using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.PlanDesignRenderDetail.Dtos;

namespace Cloud.Temp.PlanDesignRenderDetail
{
    public class PlanDesignRenderDetailAppService : CloudAppServiceBase, IPlanDesignRenderDetailAppService
    {
        private readonly IPlanDesignRenderDetailRepositories _planDesignRenderDetailRepositories;
        public PlanDesignRenderDetailAppService(IPlanDesignRenderDetailRepositories planDesignRenderDetailRepositories)
        {
            _planDesignRenderDetailRepositories = planDesignRenderDetailRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.PlanDesignRenderDetail>();
            return _planDesignRenderDetailRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _planDesignRenderDetailRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _planDesignRenderDetailRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _planDesignRenderDetailRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _planDesignRenderDetailRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _planDesignRenderDetailRepositories.ToPaging("PlanDesignRenderDetail", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<PlanDesignRenderDetailDto>>() };
        }
    }
}
                    