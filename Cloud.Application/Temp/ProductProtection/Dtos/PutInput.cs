using Abp.AutoMapper;
namespace Cloud.ProductProtection.Dtos{
[AutoMap(typeof(Domain.ProductProtection))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}