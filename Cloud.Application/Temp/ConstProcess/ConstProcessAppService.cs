using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstProcess.Dtos;

namespace Cloud.Temp.ConstProcess
{
    public class ConstProcessAppService : CloudAppServiceBase, IConstProcessAppService
    {
        private readonly IConstProcessRepositories _constProcessRepositories;
        public ConstProcessAppService(IConstProcessRepositories constProcessRepositories)
        {
            _constProcessRepositories = constProcessRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstProcess>();
            return _constProcessRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constProcessRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constProcessRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constProcessRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constProcessRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constProcessRepositories.ToPaging("ConstProcess", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstProcessDto>>() };
        }
    }
}
                    