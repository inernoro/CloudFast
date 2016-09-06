using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.ConstScoreLog.Dtos;
namespace Cloud.ConstScoreLog
{
    public class ConstScoreLogAppService : CloudAppServiceBase, IConstScoreLogAppService
    {
        private readonly IConstScoreLogRepositories _ConstScoreLogRepositories;
        public ConstScoreLogAppService(IConstScoreLogRepositories ConstScoreLogRepositories)
        {
            _ConstScoreLogRepositories = ConstScoreLogRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.ConstScoreLog>();
            return _ConstScoreLogRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _ConstScoreLogRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _ConstScoreLogRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _ConstScoreLogRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _ConstScoreLogRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _ConstScoreLogRepositories.ToPaging("ConstScoreLog", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<ConstScoreLogDto>>() };
        }
    }
}
                    