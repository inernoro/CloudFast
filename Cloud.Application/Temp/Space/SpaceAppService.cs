using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Space.Dtos;

namespace Cloud.Temp.Space
{
    public class SpaceAppService : CloudAppServiceBase, ISpaceAppService
    {
        private readonly ISpaceRepositories _spaceRepositories;
        public SpaceAppService(ISpaceRepositories spaceRepositories)
        {
            _spaceRepositories = spaceRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Space>();
            return _spaceRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _spaceRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _spaceRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _spaceRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _spaceRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _spaceRepositories.ToPaging("Space", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<SpaceDto>>() };
        }
    }
}
                    