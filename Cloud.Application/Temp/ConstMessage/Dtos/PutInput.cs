using Abp.AutoMapper;

namespace Cloud.Temp.ConstMessage.Dtos{
[AutoMap(typeof(Domain.ConstMessage))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}