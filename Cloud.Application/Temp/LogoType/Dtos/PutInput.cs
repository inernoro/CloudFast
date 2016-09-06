using Abp.AutoMapper;

namespace Cloud.Temp.LogoType.Dtos{
[AutoMap(typeof(Domain.LogoType))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}