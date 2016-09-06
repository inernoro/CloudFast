using Abp.AutoMapper;

namespace Cloud.Temp.ConstDocument.Dtos{
[AutoMap(typeof(Domain.ConstDocument))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}