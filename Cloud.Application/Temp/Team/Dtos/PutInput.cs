using Abp.AutoMapper;

namespace Cloud.Temp.Team.Dtos{
[AutoMap(typeof(Domain.Team))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}