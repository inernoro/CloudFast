using Abp.AutoMapper;
namespace Cloud.ShopAudit.Dtos{
[AutoMap(typeof(Domain.ShopAudit))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}