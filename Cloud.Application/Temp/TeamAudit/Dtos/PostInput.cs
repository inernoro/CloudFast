using System;
using Abp.AutoMapper;

namespace Cloud.Temp.TeamAudit.Dtos
{
	[AutoMap(typeof(Domain.TeamAudit))]
	public class PostInput {
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}