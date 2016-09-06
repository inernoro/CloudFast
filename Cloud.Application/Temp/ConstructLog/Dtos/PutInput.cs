using Abp.AutoMapper;

namespace Cloud.Temp.ConstructLog.Dtos{
[AutoMap(typeof(Domain.ConstructLog))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}