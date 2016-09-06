using Abp.AutoMapper;

namespace Cloud.Temp.Test.Dtos{
[AutoMap(typeof(Domain.Test))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}