using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ManagerRoleNav :Entity {
		public override int Id{ get; set; }
		public int RoleId{ get; set; }
		public string NavId{ get; set; }
		public string ActionType{ get; set; }
	}
}