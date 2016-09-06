using Abp.AutoMapper;
namespace Cloud.Tender.Dtos{
[AutoMap(typeof(Domain.Tender))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}