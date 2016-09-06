using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.ConstScoreLog.Dtos;

namespace Cloud.Temp.ConstScoreLog
{
    public class ConstScoreLogAppService : CloudAppServiceBase, IConstScoreLogAppService
    {
        private readonly IConstScoreLogRepositories _constScoreLogRepositories;
        public ConstScoreLogAppService(IConstScoreLogRepositories constScoreLogRepositories)
        {
            _constScoreLogRepositories = constScoreLogRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstScoreLog>();
            return _constScoreLogRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _constScoreLogRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _constScoreLogRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _constScoreLogRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _constScoreLogRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _constScoreLogRepositories.ToPaging("ConstScoreLog", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstScoreLogDto>>() };
        }
    }
}
                    