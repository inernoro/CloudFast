using Abp.AutoMapper;
namespace Cloud.Onlookers.Dtos{
[AutoMap(typeof(Domain.Onlookers))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}