using Abp.AutoMapper;

namespace Cloud.Temp.ChatMessage.Dtos{
[AutoMap(typeof(Domain.ChatMessage))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}