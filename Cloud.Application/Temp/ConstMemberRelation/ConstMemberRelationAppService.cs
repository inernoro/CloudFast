using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstMemberRelation.Dtos;
namespace Cloud.ConstMemberRelation
{
    public class ConstMemberRelationAppService : CloudAppServiceBase, IConstMemberRelationAppService
    {
        private readonly IConstMemberRelationRepositories _ConstMemberRelationRepositories;
        public ConstMemberRelationAppService(IConstMemberRelationRepositories ConstMemberRelationRepositories)
        {
            _ConstMemberRelationRepositories = ConstMemberRelationRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMemberRelation>();
            return _ConstMemberRelationRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstMemberRelationRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstMemberRelationRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstMemberRelationRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstMemberRelationRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstMemberRelationRepositories.ToPaging("ConstMemberRelation", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMemberRelationDto>>() };
        }
    }
}
                    