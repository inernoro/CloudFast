using System;
using Abp.AutoMapper;
namespace Cloud.Role.Dtos{
	[AutoMap(typeof(Domain.Role))]
	public class RoleDto{
		public int ID{ get; set; }
		public string Name{ get; set; }
		public int ParentId{ get; set; }
		public int RoleType{ get; set; }
		public int IsAdmin{ get; set; }
	}
}