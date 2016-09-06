using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Template.Dtos;

namespace Cloud.Temp.Template
{
    public class TemplateAppService : CloudAppServiceBase, ITemplateAppService
    {
        private readonly ITemplateRepositories _templateRepositories;
        public TemplateAppService(ITemplateRepositories templateRepositories)
        {
            _templateRepositories = templateRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Template>();
            return _templateRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _templateRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _templateRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _templateRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _templateRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _templateRepositories.ToPaging("Template", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TemplateDto>>() };
        }
    }
}
                    