using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Unit :Entity {
		public override int Id{ get; set; }
		public string Name{ get; set; }
		public int Category{ get; set; }
	}
}