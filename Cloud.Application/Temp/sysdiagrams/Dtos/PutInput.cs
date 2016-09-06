using Abp.AutoMapper;
namespace Cloud.sysdiagrams.Dtos{
[AutoMap(typeof(Domain.sysdiagrams))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}