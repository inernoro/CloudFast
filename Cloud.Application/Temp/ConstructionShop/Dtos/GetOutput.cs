using System;

namespace Cloud.Temp.ConstructionShop.Dtos {
public class GetOutput {
  
		public int ConstId{ get; set; }
		public int ShopId{ get; set; }
		public int ProprietorScore{ get; set; }
		public int Credits{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}