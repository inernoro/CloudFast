using Abp.AutoMapper;

namespace Cloud.Temp.Style.Dtos{
[AutoMap(typeof(Domain.Style))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}