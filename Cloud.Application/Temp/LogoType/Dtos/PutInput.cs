using Abp.AutoMapper;
namespace Cloud.LogoType.Dtos{
[AutoMap(typeof(Domain.LogoType))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}