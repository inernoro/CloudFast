using Abp.AutoMapper;

namespace Cloud.Temp.Onlookers.Dtos{
[AutoMap(typeof(Domain.Onlookers))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}