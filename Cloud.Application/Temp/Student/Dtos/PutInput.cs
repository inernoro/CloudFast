using Abp.AutoMapper;
namespace Cloud.Student.Dtos{
[AutoMap(typeof(Domain.Student))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}