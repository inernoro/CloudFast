using System;
using Abp.AutoMapper;

namespace Cloud.Temp.ApplyJoinShop.Dtos
{
	[AutoMap(typeof(Domain.ApplyJoinShop))]
	public class PostInput {
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public int JoinState{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}