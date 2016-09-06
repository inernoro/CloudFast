using Abp.AutoMapper;

namespace Cloud.Temp.ConstAcceptance.Dtos{
	[AutoMap(typeof(Domain.ConstAcceptance))]
	public class ConstAcceptanceDto{
		public int ConstId{ get; set; }
		public int State{ get; set; }
		public int Role{ get; set; }
		public int Progress{ get; set; }
	}
}