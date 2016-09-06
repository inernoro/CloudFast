using Abp.AutoMapper;

namespace Cloud.Temp.Bid.Dtos{
[AutoMap(typeof(Domain.Bid))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}