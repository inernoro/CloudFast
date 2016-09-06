using Abp.AutoMapper;
namespace Cloud.Team.Dtos{
[AutoMap(typeof(Domain.Team))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}