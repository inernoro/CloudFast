using Abp.AutoMapper;

namespace Cloud.Temp.Tender.Dtos{
[AutoMap(typeof(Domain.Tender))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}