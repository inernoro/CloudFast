using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Space :Entity {
		public override int Id{ get; set; }
		public string Name{ get; set; }
		public string Description{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}