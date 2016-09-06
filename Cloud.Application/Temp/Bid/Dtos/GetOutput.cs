using System;

namespace Cloud.Temp.Bid.Dtos {
public class GetOutput {
  
		public int ShopId{ get; set; }
		public int TenderId{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}