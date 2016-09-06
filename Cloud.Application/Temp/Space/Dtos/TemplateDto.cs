using System;
using Abp.AutoMapper;

namespace Cloud.Temp.Space.Dtos{
	[AutoMap(typeof(Domain.Space))]
	public class SpaceDto{
		public string Name{ get; set; }
		public string Description{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}