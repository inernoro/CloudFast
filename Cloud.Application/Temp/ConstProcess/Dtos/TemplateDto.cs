using Abp.AutoMapper;

namespace Cloud.Temp.ConstProcess.Dtos{
	[AutoMap(typeof(Domain.ConstProcess))]
	public class ConstProcessDto{
		public string Name{ get; set; }
		public string Description{ get; set; }
		public int JobCategory{ get; set; }
		public int Score{ get; set; }
		public int Measured{ get; set; }
		public int ParentId{ get; set; }
	}
}