using System;
namespace Cloud.Logo.Dtos {
public class GetOutput {
  
		public int id{ get; set; }
		public string Name{ get; set; }
		public string LogoimgUrl{ get; set; }
		public int sort{ get; set; }
		public int Enable{ get; set; }
		public int recommended{ get; set; }
		public int logotype{ get; set; }
		public string Logoimghref{ get; set; }
		public string Description{ get; set; }
		public DateTime createtime{ get; set; }  
	}
}