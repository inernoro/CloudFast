using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Test :Entity {
		public override int Id{ get; set; }
		public string Name{ get; set; }
	}
}