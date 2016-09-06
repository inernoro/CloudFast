using Abp.AutoMapper;
namespace Cloud.ConstScoreLog.Dtos{
[AutoMap(typeof(Domain.ConstScoreLog))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}