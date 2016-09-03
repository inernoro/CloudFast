using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Template.Dtos;

namespace Cloud.Template
{
    public class TemplateAppService : CloudAppServiceBase, ITemplateAppService
    {
        private readonly ITemplateRepositories _templateRepositorie;

        public TemplateAppService(ITemplateRepositories templateRepositorie)
        {
            _templateRepositorie = templateRepositorie;
        }


        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Template>();
            return _templateRepositorie.InsertAsync(model);
        }

        public Task Delete(DeletetInput input)
        {
            return _templateRepositorie.DeleteAsync(input.Id);
        }

        public Task Put(PutInput input)
        {
            var oldData = _templateRepositorie.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _templateRepositorie.UpdateAsync(newData);

        }

        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _templateRepositorie.Get(input.Id).MapTo<GetOutput>());

        }

        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _templateRepositorie.ToPaging("Template", input, "*", "Id", new { }));

            return new GetAllOutput() { Items = page.MapTo<IEnumerable<TemplateDto>>() };

        }
    }
}