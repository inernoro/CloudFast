using System;
namespace Cloud.FloorPlanDetail.Dtos {
public class GetOutput {
  
		public int LocalPlanId{ get; set; }
		public int UserInfoUid{ get; set; }
		public string ObsPlanId{ get; set; }
		public string ObsDesignId{ get; set; }
		public string CommName{ get; set; }
		public string Name{ get; set; }
		public string SpecName{ get; set; }
		public double SrcArea{ get; set; }
		public double Area{ get; set; }
		public string PlanCity{ get; set; }
		public string PlanPic{ get; set; }
		public string PlanSmallPic{ get; set; }  
	}
}