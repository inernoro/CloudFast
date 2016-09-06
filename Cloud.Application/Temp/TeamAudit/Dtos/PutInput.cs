using Abp.AutoMapper;

namespace Cloud.Temp.TeamAudit.Dtos{
[AutoMap(typeof(Domain.TeamAudit))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}