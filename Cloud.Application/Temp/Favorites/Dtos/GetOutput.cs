using System;
namespace Cloud.Favorites.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public int Category{ get; set; }
		public int PrimaryKey{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}