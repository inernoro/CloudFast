using System;
using Abp.AutoMapper;
namespace Cloud.Style.Dtos
{
	[AutoMap(typeof(Domain.Style))]
	public class PostInput {
		public string Name{ get; set; }
		public string Description{ get; set; }
		public string Url{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}