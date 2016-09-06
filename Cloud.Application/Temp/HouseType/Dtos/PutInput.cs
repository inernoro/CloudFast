using Abp.AutoMapper;
namespace Cloud.HouseType.Dtos{
[AutoMap(typeof(Domain.HouseType))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}