using System;

namespace Cloud.Temp.ConstMemberRelation.Dtos {
public class GetOutput {
  
		public int ConstId{ get; set; }
		public int UserId{ get; set; }
		public DateTime CreateTime{ get; set; }
		public int Credits{ get; set; }
		public int ProprietorScore{ get; set; }  
	}
}