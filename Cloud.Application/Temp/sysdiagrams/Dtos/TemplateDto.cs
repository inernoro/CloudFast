using Abp.AutoMapper;

namespace Cloud.Temp.sysdiagrams.Dtos{
	[AutoMap(typeof(Domain.sysdiagrams))]
	public class SysdiagramsDto{
		public string Name{ get; set; }
		public int PrincipalId{ get; set; }
		public int DiagramId{ get; set; }
		public int Version{ get; set; }
		public string Definition{ get; set; }
	}
}