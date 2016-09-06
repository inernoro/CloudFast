using Abp.AutoMapper;
namespace Cloud.Unit.Dtos{
[AutoMap(typeof(Domain.Unit))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}