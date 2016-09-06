using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Role :Entity {
		public int ID{ get; set; }
		public string Name{ get; set; }
		public int ParentId{ get; set; }
		public int RoleType{ get; set; }
		public int IsAdmin{ get; set; }
	}
}