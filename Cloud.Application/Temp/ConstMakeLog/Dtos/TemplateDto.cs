using System;
using Abp.AutoMapper;

namespace Cloud.Temp.ConstMakeLog.Dtos{
	[AutoMap(typeof(Domain.ConstMakeLog))]
	public class ConstMakeLogDto{
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public int MakeId{ get; set; }
		public int ControlState{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
		public string Description{ get; set; }
	}
}