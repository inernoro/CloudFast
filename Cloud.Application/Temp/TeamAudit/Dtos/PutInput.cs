using Abp.AutoMapper;
namespace Cloud.TeamAudit.Dtos{
[AutoMap(typeof(Domain.TeamAudit))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}