using Abp.AutoMapper;

namespace Cloud.Temp.Knowledge.Dtos{
[AutoMap(typeof(Domain.Knowledge))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}