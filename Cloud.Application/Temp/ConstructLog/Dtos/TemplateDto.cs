using System;
using Abp.AutoMapper;

namespace Cloud.Temp.ConstructLog.Dtos{
	[AutoMap(typeof(Domain.ConstructLog))]
	public class ConstructLogDto{
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