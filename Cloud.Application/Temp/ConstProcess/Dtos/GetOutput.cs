using System;
namespace Cloud.ConstProcess.Dtos {
public class GetOutput {
  
		public string Name{ get; set; }
		public string Description{ get; set; }
		public int JobCategory{ get; set; }
		public int Score{ get; set; }
		public int Measured{ get; set; }
		public int ParentId{ get; set; }  
	}
}