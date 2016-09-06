using System;

namespace Cloud.Temp.ConstDocument.Dtos {
public class GetOutput {
  
		public int ConstId{ get; set; }
		public int Category{ get; set; }
		public string Title{ get; set; }
		public string Url{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}