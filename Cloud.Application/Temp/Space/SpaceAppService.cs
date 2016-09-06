using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Space.Dtos;
namespace Cloud.Space
{
    public class SpaceAppService : CloudAppServiceBase, ISpaceAppService
    {
        private readonly ISpaceRepositories _SpaceRepositories;
        public SpaceAppService(ISpaceRepositories SpaceRepositories)
        {
            _SpaceRepositories = SpaceRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Space>();
            return _SpaceRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _SpaceRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _SpaceRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _SpaceRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _SpaceRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _SpaceRepositories.ToPaging("Space", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<SpaceDto>>() };
        }
    }
}
                    