using Abp.AutoMapper;

namespace Cloud.Temp.Space.Dtos{
[AutoMap(typeof(Domain.Space))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}