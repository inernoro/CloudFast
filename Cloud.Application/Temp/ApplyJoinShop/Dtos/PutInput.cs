using Abp.AutoMapper;
namespace Cloud.ApplyJoinShop.Dtos{
[AutoMap(typeof(Domain.ApplyJoinShop))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}