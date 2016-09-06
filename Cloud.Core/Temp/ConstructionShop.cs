using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstructionShop :Entity {
		public override int Id{ get; set; }
		public int ConstId{ get; set; }
		public int ShopId{ get; set; }
		public int ProprietorScore{ get; set; }
		public int Credits{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}