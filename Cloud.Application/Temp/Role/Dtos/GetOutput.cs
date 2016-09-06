namespace Cloud.Temp.Role.Dtos {
public class GetOutput {
  
		public int Id{ get; set; }
		public string Name{ get; set; }
		public int ParentId{ get; set; }
		public int RoleType{ get; set; }
		public int IsAdmin{ get; set; }  
	}
}