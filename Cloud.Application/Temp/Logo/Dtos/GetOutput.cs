using System;

namespace Cloud.Temp.Logo.Dtos {
public class GetOutput {
  
		public int Id{ get; set; }
		public string Name{ get; set; }
		public string LogoimgUrl{ get; set; }
		public int Sort{ get; set; }
		public int Enable{ get; set; }
		public int Recommended{ get; set; }
		public int Logotype{ get; set; }
		public string Logoimghref{ get; set; }
		public string Description{ get; set; }
		public DateTime Createtime{ get; set; }  
	}
}