using System;

namespace Cloud.Temp.ApplyJoinShop.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public int JoinState{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}