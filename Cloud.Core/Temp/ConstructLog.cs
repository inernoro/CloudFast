using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstructLog :Entity {
		public override int Id{ get; set; }
		public int ConstId{ get; set; }
		public int UserId{ get; set; }
		public string Description{ get; set; }
		public string Img{ get; set; }
		public string Audio{ get; set; }
		public string Video{ get; set; }
		public int Category{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}