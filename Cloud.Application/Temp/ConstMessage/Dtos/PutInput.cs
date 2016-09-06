using Abp.AutoMapper;
namespace Cloud.ConstMessage.Dtos{
[AutoMap(typeof(Domain.ConstMessage))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}