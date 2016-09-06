using Abp.AutoMapper;

namespace Cloud.Temp.City.Dtos{
[AutoMap(typeof(Domain.City))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}