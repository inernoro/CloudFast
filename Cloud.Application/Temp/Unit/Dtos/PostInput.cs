using System;
using Abp.AutoMapper;
namespace Cloud.Unit.Dtos
{
	[AutoMap(typeof(Domain.Unit))]
	public class PostInput {
		public string Name{ get; set; }
		public int Category{ get; set; }
	}
}