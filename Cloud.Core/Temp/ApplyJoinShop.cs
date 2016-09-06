using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ApplyJoinShop :Entity {
		public override int Id{ get; set; }
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public int JoinState{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}