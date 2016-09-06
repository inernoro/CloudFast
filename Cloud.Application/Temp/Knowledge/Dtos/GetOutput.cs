using System;

namespace Cloud.Temp.Knowledge.Dtos {
public class GetOutput {
  
		public int Category{ get; set; }
		public string Title{ get; set; }
		public string Alt{ get; set; }
		public string CategoryName{ get; set; }
		public string Details{ get; set; }
		public string Url{ get; set; }
		public int Creator{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}