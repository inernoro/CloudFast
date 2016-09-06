using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstMakeQuestion.Dtos;
namespace Cloud.ConstMakeQuestion
{
    public class ConstMakeQuestionAppService : CloudAppServiceBase, IConstMakeQuestionAppService
    {
        private readonly IConstMakeQuestionRepositories _ConstMakeQuestionRepositories;
        public ConstMakeQuestionAppService(IConstMakeQuestionRepositories ConstMakeQuestionRepositories)
        {
            _ConstMakeQuestionRepositories = ConstMakeQuestionRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMakeQuestion>();
            return _ConstMakeQuestionRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstMakeQuestionRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstMakeQuestionRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstMakeQuestionRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstMakeQuestionRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstMakeQuestionRepositories.ToPaging("ConstMakeQuestion", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMakeQuestionDto>>() };
        }
    }
}
                    