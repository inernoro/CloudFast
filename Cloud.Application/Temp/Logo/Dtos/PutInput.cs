using Abp.AutoMapper;
namespace Cloud.Logo.Dtos{
[AutoMap(typeof(Domain.Logo))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}