using System;
namespace Cloud.sysdiagrams.Dtos {
public class GetOutput {
  
		public string name{ get; set; }
		public int principal_id{ get; set; }
		public int diagram_id{ get; set; }
		public int version{ get; set; }
		public string definition{ get; set; }  
	}
}