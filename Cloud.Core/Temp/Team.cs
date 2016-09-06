using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Team :Entity {
		public override int Id{ get; set; }
		public int UserId{ get; set; }
		public int ShopId{ get; set; }
		public string Master{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}