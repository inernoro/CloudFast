using System;
using Abp.AutoMapper;

namespace Cloud.Temp.LogoType.Dtos{
	[AutoMap(typeof(Domain.LogoType))]
	public class LogoTypeDto{
		public int Id{ get; set; }
		public string Name{ get; set; }
		public DateTime Cratetime{ get; set; }
	}
}