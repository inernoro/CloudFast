using System;
namespace Cloud.ConstContract.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public string Number{ get; set; }
		public string Address{ get; set; }
		public double ConstCost{ get; set; }
		public double ConstArea{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}