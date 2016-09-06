using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Bid :Entity {
		public override int Id{ get; set; }
		public int ShopId{ get; set; }
		public int TenderId{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}