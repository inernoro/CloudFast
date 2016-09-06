using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstProcess :Entity {
		public override int Id{ get; set; }
		public string Name{ get; set; }
		public string Description{ get; set; }
		public int JobCategory{ get; set; }
		public int Score{ get; set; }
		public int Measured{ get; set; }
		public int ParentId{ get; set; }
	}
}