using Abp.AutoMapper;
namespace Cloud.Space.Dtos{
[AutoMap(typeof(Domain.Space))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}