using System;

namespace Cloud.Temp.ProductProtection.Dtos {
public class GetOutput {
  
		public int ConstId{ get; set; }
		public int Score{ get; set; }
		public int State{ get; set; }
		public string Evaluation{ get; set; }
		public DateTime CreateTime{ get; set; }
		public string Image{ get; set; }  
	}
}