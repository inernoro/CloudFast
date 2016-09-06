using System;
using Abp.AutoMapper;
namespace Cloud.Unit.Dtos{
	[AutoMap(typeof(Domain.Unit))]
	public class UnitDto{
		public string Name{ get; set; }
		public int Category{ get; set; }
	}
}