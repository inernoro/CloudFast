using System;

namespace Cloud.Temp.Shop.Dtos {
public class GetOutput {
  
		public string Name{ get; set; }
		public string Logo{ get; set; }
		public int Cityid{ get; set; }
		public string Address{ get; set; }
		public int Enable{ get; set; }
		public int Category{ get; set; }
		public string Description{ get; set; }
		public int PlatformAuth{ get; set; }
		public int RealnameAuth{ get; set; }
		public int Star{ get; set; }
		public DateTime CreateTime{ get; set; }
		public int QualifiedRate{ get; set; }
		public int AverageScore{ get; set; }
		public int TotalScore{ get; set; }
		public int Complete{ get; set; }
		public int NotComplete{ get; set; }
		public string Company{ get; set; }  
	}
}