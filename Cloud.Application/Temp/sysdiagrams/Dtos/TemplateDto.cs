using System;
using Abp.AutoMapper;
namespace Cloud.sysdiagrams.Dtos{
	[AutoMap(typeof(Domain.sysdiagrams))]
	public class sysdiagramsDto{
		public string name{ get; set; }
		public int principal_id{ get; set; }
		public int diagram_id{ get; set; }
		public int version{ get; set; }
		public string definition{ get; set; }
	}
}