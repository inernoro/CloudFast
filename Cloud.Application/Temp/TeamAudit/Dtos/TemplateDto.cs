using System;
using Abp.AutoMapper;
namespace Cloud.TeamAudit.Dtos{
	[AutoMap(typeof(Domain.TeamAudit))]
	public class TeamAuditDto{
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}