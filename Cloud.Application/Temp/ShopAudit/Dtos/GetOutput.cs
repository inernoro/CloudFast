using System;
namespace Cloud.ShopAudit.Dtos {
public class GetOutput {
  
		public int ShopType{ get; set; }
		public int State{ get; set; }
		public int UserId{ get; set; }
		public DateTime CreateTime{ get; set; }
		public string ShopName{ get; set; }
		public int ShopCity{ get; set; }
		public string ShopAddress{ get; set; }
		public string ShopDescript{ get; set; }  
	}
}