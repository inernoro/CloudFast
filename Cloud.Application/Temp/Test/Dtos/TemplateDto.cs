using Abp.AutoMapper;

namespace Cloud.Temp.Test.Dtos{
	[AutoMap(typeof(Domain.Test))]
	public class TestDto{
		public string Name{ get; set; }
	}
}