using Abp.AutoMapper;

namespace Cloud.Temp.ConstScoreLog.Dtos{
[AutoMap(typeof(Domain.ConstScoreLog))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}