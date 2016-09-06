using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstDocument :Entity {
		public override int Id{ get; set; }
		public int ConstId{ get; set; }
		public int Category{ get; set; }
		public string Title{ get; set; }
		public string Url{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}