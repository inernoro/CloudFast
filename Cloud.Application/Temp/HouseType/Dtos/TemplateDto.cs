using System;
using Abp.AutoMapper;
namespace Cloud.HouseType.Dtos{
	[AutoMap(typeof(Domain.HouseType))]
	public class HouseTypeDto{
		public string Name{ get; set; }
		public string Description{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}