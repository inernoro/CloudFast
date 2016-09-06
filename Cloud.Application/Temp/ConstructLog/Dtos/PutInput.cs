using Abp.AutoMapper;
namespace Cloud.ConstructLog.Dtos{
[AutoMap(typeof(Domain.ConstructLog))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}