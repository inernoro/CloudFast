using System;
namespace Cloud.PlanDesignRenderDetail.Dtos {
public class GetOutput {
  
		public int RenderId{ get; set; }
		public int LocalPlanId{ get; set; }
		public string ObsPlanId{ get; set; }
		public string ObsDesignId{ get; set; }
		public string Img{ get; set; }
		public string SmallImg{ get; set; }
		public string PicId{ get; set; }
		public int PicType{ get; set; }
		public string RoomTypeName{ get; set; }  
	}
}