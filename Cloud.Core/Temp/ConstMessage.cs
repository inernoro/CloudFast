using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstMessage :Entity {
		public override int Id{ get; set; }
		public int Category{ get; set; }
		public string Description{ get; set; }
		public int Role{ get; set; }
		public int ShopId{ get; set; }
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public int ConstMakeId{ get; set; }
		public int ReadState{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}