using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstProcess.Dtos;
namespace Cloud.ConstProcess
{
    public class ConstProcessAppService : CloudAppServiceBase, IConstProcessAppService
    {
        private readonly IConstProcessRepositories _ConstProcessRepositories;
        public ConstProcessAppService(IConstProcessRepositories ConstProcessRepositories)
        {
            _ConstProcessRepositories = ConstProcessRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstProcess>();
            return _ConstProcessRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstProcessRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstProcessRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstProcessRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstProcessRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstProcessRepositories.ToPaging("ConstProcess", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstProcessDto>>() };
        }
    }
}
                    