using System;
using Abp.AutoMapper;
namespace Cloud.UnitCategory.Dtos
{
	[AutoMap(typeof(Domain.UnitCategory))]
	public class PostInput {
		public string Name{ get; set; }
	}
}