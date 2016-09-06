using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Student.Dtos;

namespace Cloud.Temp.Student
{
    public class StudentAppService : CloudAppServiceBase, IStudentAppService
    {
        private readonly IStudentRepositories _studentRepositories;
        public StudentAppService(IStudentRepositories studentRepositories)
        {
            _studentRepositories = studentRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Student>();
            return _studentRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _studentRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _studentRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _studentRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _studentRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _studentRepositories.ToPaging("Student", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<StudentDto>>() };
        }
    }
}
                    