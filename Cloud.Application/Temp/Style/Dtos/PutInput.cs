using Abp.AutoMapper;
namespace Cloud.Style.Dtos{
[AutoMap(typeof(Domain.Style))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}