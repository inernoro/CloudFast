using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.StandardProcedure.Dtos;
namespace Cloud.StandardProcedure
{
    public class StandardProcedureAppService : CloudAppServiceBase, IStandardProcedureAppService
    {
        private readonly IStandardProcedureRepositories _StandardProcedureRepositories;
        public StandardProcedureAppService(IStandardProcedureRepositories StandardProcedureRepositories)
        {
            _StandardProcedureRepositories = StandardProcedureRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.StandardProcedure>();
            return _StandardProcedureRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _StandardProcedureRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _StandardProcedureRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _StandardProcedureRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _StandardProcedureRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _StandardProcedureRepositories.ToPaging("StandardProcedure", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<StandardProcedureDto>>() };
        }
    }
}
                    