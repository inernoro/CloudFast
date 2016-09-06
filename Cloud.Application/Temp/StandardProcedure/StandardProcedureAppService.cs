using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.StandardProcedure.Dtos;

namespace Cloud.Temp.StandardProcedure
{
    public class StandardProcedureAppService : CloudAppServiceBase, IStandardProcedureAppService
    {
        private readonly IStandardProcedureRepositories _standardProcedureRepositories;
        public StandardProcedureAppService(IStandardProcedureRepositories standardProcedureRepositories)
        {
            _standardProcedureRepositories = standardProcedureRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.StandardProcedure>();
            return _standardProcedureRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _standardProcedureRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _standardProcedureRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _standardProcedureRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _standardProcedureRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _standardProcedureRepositories.ToPaging("StandardProcedure", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<StandardProcedureDto>>() };
        }
    }
}
                    