using Abp.AutoMapper;

namespace Cloud.Temp.sysdiagrams.Dtos{
[AutoMap(typeof(Domain.sysdiagrams))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}