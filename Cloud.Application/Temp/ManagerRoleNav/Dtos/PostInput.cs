using Abp.AutoMapper;

namespace Cloud.Temp.ManagerRoleNav.Dtos
{
	[AutoMap(typeof(Domain.ManagerRoleNav))]
	public class PostInput {
		public int RoleId{ get; set; }
		public string NavId{ get; set; }
		public string ActionType{ get; set; }
	}
}