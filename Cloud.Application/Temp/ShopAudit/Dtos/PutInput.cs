using Abp.AutoMapper;

namespace Cloud.Temp.ShopAudit.Dtos{
[AutoMap(typeof(Domain.ShopAudit))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}