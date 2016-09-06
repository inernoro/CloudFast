using Abp.AutoMapper;
namespace Cloud.Construct.Dtos{
[AutoMap(typeof(Domain.Construct))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}