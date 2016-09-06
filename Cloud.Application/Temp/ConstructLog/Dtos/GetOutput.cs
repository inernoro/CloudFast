using System;

namespace Cloud.Temp.ConstructLog.Dtos {
public class GetOutput {
  
		public int ConstId{ get; set; }
		public int UserId{ get; set; }
		public string Description{ get; set; }
		public string Img{ get; set; }
		public string Audio{ get; set; }
		public string Video{ get; set; }
		public int Category{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}