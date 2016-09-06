using System;
using Abp.AutoMapper;
namespace Cloud.ConstScoreLog.Dtos
{
	[AutoMap(typeof(Domain.ConstScoreLog))]
	public class PostInput {
		public int ConstId{ get; set; }
		public int PrimaryKey{ get; set; }
		public int Category{ get; set; }
		public int DeclineScore{ get; set; }
		public string Description{ get; set; }
		public string Image{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}