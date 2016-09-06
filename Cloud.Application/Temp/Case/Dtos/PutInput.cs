using Abp.AutoMapper;

namespace Cloud.Temp.Case.Dtos{
[AutoMap(typeof(Domain.Case))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}