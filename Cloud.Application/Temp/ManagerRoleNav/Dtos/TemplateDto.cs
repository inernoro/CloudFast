using System;
using Abp.AutoMapper;
namespace Cloud.ManagerRoleNav.Dtos{
	[AutoMap(typeof(Domain.ManagerRoleNav))]
	public class ManagerRoleNavDto{
		public int RoleId{ get; set; }
		public string NavId{ get; set; }
		public string ActionType{ get; set; }
	}
}