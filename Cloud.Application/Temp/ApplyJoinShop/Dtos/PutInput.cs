using Abp.AutoMapper;

namespace Cloud.Temp.ApplyJoinShop.Dtos{
[AutoMap(typeof(Domain.ApplyJoinShop))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}