using Abp.AutoMapper;
namespace Cloud.ConstructionShop.Dtos{
[AutoMap(typeof(Domain.ConstructionShop))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}