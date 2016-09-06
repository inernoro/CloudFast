using System;
using Abp.AutoMapper;
namespace Cloud.Space.Dtos
{
	[AutoMap(typeof(Domain.Space))]
	public class PostInput {
		public string Name{ get; set; }
		public string Description{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}