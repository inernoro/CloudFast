using Abp.AutoMapper;

namespace Cloud.Temp.Test.Dtos
{
	[AutoMap(typeof(Domain.Test))]
	public class PostInput {
		public string Name{ get; set; }
	}
}