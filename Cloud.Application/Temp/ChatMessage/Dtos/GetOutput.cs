using System;
namespace Cloud.ChatMessage.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public string MessageText{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}