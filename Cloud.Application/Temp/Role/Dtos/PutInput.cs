using Abp.AutoMapper;

namespace Cloud.Temp.Role.Dtos{
[AutoMap(typeof(Domain.Role))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}