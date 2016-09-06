using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstMakeQuestion.Dtos;

namespace Cloud.Temp.ConstMakeQuestion
{
    public class ConstMakeQuestionAppService : CloudAppServiceBase, IConstMakeQuestionAppService
    {
        private readonly IConstMakeQuestionRepositories _constMakeQuestionRepositories;
        public ConstMakeQuestionAppService(IConstMakeQuestionRepositories constMakeQuestionRepositories)
        {
            _constMakeQuestionRepositories = constMakeQuestionRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstMakeQuestion>();
            return _constMakeQuestionRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constMakeQuestionRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constMakeQuestionRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constMakeQuestionRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constMakeQuestionRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constMakeQuestionRepositories.ToPaging("ConstMakeQuestion", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstMakeQuestionDto>>() };
        }
    }
}
                    