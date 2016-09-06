using System;
using Abp.AutoMapper;
namespace Cloud.ChatMessage.Dtos{
	[AutoMap(typeof(Domain.ChatMessage))]
	public class ChatMessageDto{
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public string MessageText{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}