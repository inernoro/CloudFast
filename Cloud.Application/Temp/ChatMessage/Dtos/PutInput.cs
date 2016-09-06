using Abp.AutoMapper;
namespace Cloud.ChatMessage.Dtos{
[AutoMap(typeof(Domain.ChatMessage))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}