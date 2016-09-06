using Abp.AutoMapper;

namespace Cloud.Temp.UnitCategory.Dtos{
	[AutoMap(typeof(Domain.UnitCategory))]
	public class UnitCategoryDto{
		public string Name{ get; set; }
	}
}