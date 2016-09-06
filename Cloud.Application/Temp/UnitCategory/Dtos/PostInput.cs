using Abp.AutoMapper;

namespace Cloud.Temp.UnitCategory.Dtos
{
	[AutoMap(typeof(Domain.UnitCategory))]
	public class PostInput {
		public string Name{ get; set; }
	}
}