using System;
namespace Cloud.UserInfo.Dtos {
public class GetOutput {
  
		public string UserName{ get; set; }
		public string Password{ get; set; }
		public string Phone{ get; set; }
		public string Email{ get; set; }
		public string HeadPortrait{ get; set; }
		public int Role{ get; set; }
		public int Enable{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}