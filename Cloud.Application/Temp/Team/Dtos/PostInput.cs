using System;
using Abp.AutoMapper;
namespace Cloud.Team.Dtos
{
	[AutoMap(typeof(Domain.Team))]
	public class PostInput {
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public string Master{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}