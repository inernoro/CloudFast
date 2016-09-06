using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Student.Dtos;
namespace Cloud.Student
{
    public class StudentAppService : CloudAppServiceBase, IStudentAppService
    {
        private readonly IStudentRepositories _StudentRepositories;
        public StudentAppService(IStudentRepositories StudentRepositories)
        {
            _StudentRepositories = StudentRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Student>();
            return _StudentRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _StudentRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _StudentRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _StudentRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _StudentRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _StudentRepositories.ToPaging("Student", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<StudentDto>>() };
        }
    }
}
                    