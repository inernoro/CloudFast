using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstMemberRelation.Dtos;

namespace Cloud.Temp.ConstMemberRelation
{
    public class ConstMemberRelationAppService : CloudAppServiceBase, IConstMemberRelationAppService
    {
        private readonly IConstMemberRelationRepositories _constMemberRelationRepositories;
        public ConstMemberRelationAppService(IConstMemberRelationRepositories constMemberRelationRepositories)
        {
            _constMemberRelationRepositories = constMemberRelationRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMemberRelation>();
            return _constMemberRelationRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constMemberRelationRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constMemberRelationRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constMemberRelationRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constMemberRelationRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constMemberRelationRepositories.ToPaging("ConstMemberRelation", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMemberRelationDto>>() };
        }
    }
}
                    