using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstAcceptance :Entity {
		public override int Id{ get; set; }
		public int ConstId{ get; set; }
		public int State{ get; set; }
		public int Role{ get; set; }
		public int Progress{ get; set; }
	}
}