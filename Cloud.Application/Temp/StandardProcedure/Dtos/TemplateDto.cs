using System;
using Abp.AutoMapper;
namespace Cloud.StandardProcedure.Dtos{
	[AutoMap(typeof(Domain.StandardProcedure))]
	public class StandardProcedureDto{
		public int ProcessId{ get; set; }
		public string Name{ get; set; }
		public string Unit{ get; set; }
		public string Error{ get; set; }
		public double Min{ get; set; }
		public double Max{ get; set; }
		public string Description{ get; set; }
		public int Enable{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}