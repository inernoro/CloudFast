using Abp.AutoMapper;

namespace Cloud.Temp.Role.Dtos
{
	[AutoMap(typeof(Domain.Role))]
	public class PostInput {
		public int Id{ get; set; }
		public string Name{ get; set; }
		public int ParentId{ get; set; }
		public int RoleType{ get; set; }
		public int IsAdmin{ get; set; }
	}
}