using Abp.AutoMapper;
namespace Cloud.Shop.Dtos{
[AutoMap(typeof(Domain.Shop))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}