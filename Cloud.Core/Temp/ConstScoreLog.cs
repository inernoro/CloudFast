using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstScoreLog :Entity {
		public override int Id{ get; set; }
		public int ConstId{ get; set; }
		public int PrimaryKey{ get; set; }
		public int Category{ get; set; }
		public int DeclineScore{ get; set; }
		public string Description{ get; set; }
		public string Image{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}