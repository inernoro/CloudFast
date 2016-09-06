using Abp.AutoMapper;

namespace Cloud.Temp.Unit.Dtos{
	[AutoMap(typeof(Domain.Unit))]
	public class UnitDto{
		public string Name{ get; set; }
		public int Category{ get; set; }
	}
}