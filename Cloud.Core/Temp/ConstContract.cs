using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstContract :Entity {
		public override int Id{ get; set; }
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public string Number{ get; set; }
		public string Address{ get; set; }
		public double ConstCost{ get; set; }
		public double ConstArea{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}