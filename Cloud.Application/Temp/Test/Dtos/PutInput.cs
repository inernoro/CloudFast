using Abp.AutoMapper;
namespace Cloud.Test.Dtos{
[AutoMap(typeof(Domain.Test))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}