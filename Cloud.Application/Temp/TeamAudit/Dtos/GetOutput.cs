using System;

namespace Cloud.Temp.TeamAudit.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}