using Abp.AutoMapper;
namespace Cloud.Role.Dtos{
[AutoMap(typeof(Domain.Role))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}